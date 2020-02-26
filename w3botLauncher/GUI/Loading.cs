using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using w3bot.Command;
using w3botLauncher.Service;
using w3botLauncher.Utils;

namespace w3bot.GUI
{
    public partial class Loading : Form
    {
        private WebClient _webClient;
        private bool isFinished = false;
        private List<ICommand> _commandList = new List<ICommand>();
        private string APPLICATION_NAME = BotDirectories.baseDir + @"\w3bot.exe";

        public Loading()
        {
            InitializeComponent();
            LoadingBackgroundWorker.WorkerReportsProgress = true;
            LoadingBackgroundWorker.WorkerSupportsCancellation = true;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            statusLabelMessage.Text = "";
            fileDataReceivedLabel.Text = "";

            _webClient = new WebClient();
            var botDirectories = new BotDirectories();
            var fileProcess = new FileProcess();
            var downloadFactory = new DownloadFactory(_webClient, fileProcess);
            var downloadService = new DownloadService(downloadFactory);
            var tesseractDownload = downloadService.Create(DownloadType.Tesseract);
            var clientDownload = downloadService.Create(DownloadType.Client);

            _commandList.Add(botDirectories);
            _commandList.Add(tesseractDownload);
            _commandList.Add(clientDownload);

            if (LoadingBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                LoadingBackgroundWorker.RunWorkerAsync();
            }
        }

        private void SetStatusLabel(string message)
        {
            statusLabelMessage.Text = message;
        }

        private void SetFileDataReceivedLabel(string message)
        {
            fileDataReceivedLabel.Text = message;
        }

        private void LoadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var handled = false;
            ICommand command = null;

            while (!isFinished)
            {
                if (handled)
                {
                    _commandList.Remove(command);
                    handled = !handled;
                }

                if (_commandList.Count == 0)
                    isFinished = true;

                foreach (var c in _commandList)
                {
                    c.Execute();

                    if (c.IsHandled)
                    {
                        command = c;
                        handled = true;
                        continue;
                    }

                    if (c.Process == null)
                        continue;

                    statusLabelMessage.Invoke(new MethodInvoker(delegate
                    {
                        SetStatusLabel(c.Status);
                    }));
                    fileDataReceivedLabel.Invoke(new MethodInvoker(delegate
                    {
                        SetFileDataReceivedLabel(String.Format("{0} / {1} MB", Math.Round(c.Process.BytesReceived / 1000000.0, 2), Math.Round(c.Process.TotalBytesToReceive / 1000000.0, 2)));
                    }));

                    if (c.IsRunning)
                        break;
                }

                Thread.Sleep(100);
            }
        }

        private void LoadingBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void LoadingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                statusLabelMessage.Text = "Starting w3bot...";
                Process.Start(APPLICATION_NAME);
                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
