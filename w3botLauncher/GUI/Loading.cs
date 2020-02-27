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
using w3botLauncher.Command;
using w3botLauncher.Service;
using w3botLauncher.Service.Factory;
using w3botLauncher.Utils;

namespace w3botLauncher.GUI
{
    public partial class Loading : Form
    {
        private List<ICommand> _commandList = new List<ICommand>();
        private string APPLICATION_NAME = Directory.GetCurrentDirectory() + @"\w3bot\w3bot.exe";

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

            var webClient = new WebClient();
            var fileProcess = new FileProcess();
            var botDirectories = new CreateBotDirectoriesCommand();
            var downloadFactory = new DownloadFactory(webClient, fileProcess);
            var downloadService = new DownloadService(downloadFactory);
            var extractFactory = new ExtractFactory();
            var extractService = new ExtractService(extractFactory);
            var tesseractDownload = downloadService.Create(FileType.Tesseract);
            var clientDownload = downloadService.Create(FileType.Client);
            var tesseractExtract = extractService.Create(FileType.Tesseract);
            var clientExtract = extractService.Create(FileType.Client);

            _commandList.Add(botDirectories);
            _commandList.Add(tesseractDownload);
            _commandList.Add(clientDownload);
            _commandList.Add(tesseractExtract);
            _commandList.Add(clientExtract);

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
            foreach (var c in _commandList)
            {
                Execute(c);
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

        private void Execute(ICommand c)
        {
            while (!c.IsHandled || c.IsRunning)
            {
                c.Execute();

                statusLabelMessage.Invoke(new MethodInvoker(delegate
                {
                    SetStatusLabel(c.Status);
                }));

                if (c is IFileCommand)
                {
                    var file = (IFileCommand)c;
                    fileDataReceivedLabel.Invoke(new MethodInvoker(delegate
                    {
                        SetFileDataReceivedLabel(String.Format("{0} / {1} MB", Math.Round(file.Process.BytesReceived / 1000000.0, 2), Math.Round(file.Process.TotalBytesToReceive / 1000000.0, 2)));
                    }));
                }

                Thread.Sleep(100);
            }
        }
    }
}
