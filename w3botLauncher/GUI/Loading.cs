using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
        private const string APPLICATION_NAME = "w3bot.exe";
        private string _currentDirectory = Directory.GetCurrentDirectory();
        private string _installPath;
        private WebClient _webClient;

        public Loading()
        {
            InitializeComponent();
            LoadingBackgroundWorker.WorkerReportsProgress = true;
            LoadingBackgroundWorker.WorkerSupportsCancellation = true;

            _webClient = new WebClient();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            if (InstallPathExists() && !IsNewVersionAvailable())
                ExecuteApplication();
            else if (IsNewVersionAvailable())
                NotifyNewVersionIsAvailable();

            statusLabelMessage.Text = "";
            fileDataReceivedLabel.Text = "";
            
            var fileProcess = new FileProcess();
            var botDirectories = new CreateBotDirectoriesCommand();
            var downloadFactory = new DownloadFactory(_webClient, fileProcess);
            var downloadService = new DownloadService(downloadFactory);
            var extractFactory = new ExtractFactory();
            var extractService = new ExtractService(extractFactory);
            var removeFactory = new RemoveFactory();
            var removeService = new RemoveService(removeFactory);
            var tesseractDownload = downloadService.Create(FileType.Tesseract);
            var clientDownload = downloadService.Create(FileType.Client);
            var tesseractExtract = extractService.Create(FileType.Tesseract);
            var clientExtract = extractService.Create(FileType.Client);
            var tesseractRemove = removeService.Create(FileType.Tesseract, _currentDirectory);
            var clientRemove = removeService.Create(FileType.Client, _currentDirectory);

            _commandList.Add(botDirectories);
            _commandList.Add(tesseractDownload);
            _commandList.Add(clientDownload);
            _commandList.Add(tesseractExtract);
            _commandList.Add(clientExtract);
            _commandList.Add(tesseractRemove);
            _commandList.Add(clientRemove);

            Run();
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
            ExecuteApplication();
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

        private bool InstallPathExists()
        {
            try
            {
                if (!RegistryUtils.IsRegistrySubKeyAvailable() || !RegistryUtils.IsRegistryEntryAvailable())
                {
                    if (LoadingFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        _installPath = LoadingFolderBrowserDialog.SelectedPath;
                        RegistryUtils.CreateRegistryEntry(_installPath);

                        return false;
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    var key = Registry.CurrentUser.OpenSubKey(RegistryUtils.REGISTRY_SUBKEY);
                    _installPath = key.GetValue(RegistryUtils.REGISTRY_VALUE_TYPE).ToString();

                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

        private bool IsNewVersionAvailable()
        {
            try
            {
                if (!File.Exists(String.Format(@"{0}\{1}", _installPath, APPLICATION_NAME)))
                    return false;

                var currentVersion = double.Parse(_webClient.DownloadString(Connection.ENDPOINT + "version.txt"));
                var appDomain = AppDomain.CreateDomain(nameof(VersionLoader), AppDomain.CurrentDomain.Evidence, new AppDomainSetup { ApplicationBase = Path.GetDirectoryName(typeof(VersionLoader).Assembly.Location) });
                var loader = (VersionLoader)appDomain.CreateInstanceAndUnwrap(typeof(VersionLoader).Assembly.FullName, typeof(VersionLoader).FullName);
                loader.Load(String.Format(@"{0}\{1}", _installPath, APPLICATION_NAME));
                var clientAssemblyVersion = loader.Version;
                var clientVersion = float.Parse(clientAssemblyVersion.Major + "." + clientAssemblyVersion.Minor + "." + clientAssemblyVersion.Build + "." + clientAssemblyVersion.Revision);
                AppDomain.Unload(appDomain);

                if (currentVersion > clientVersion)
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

        private void NotifyNewVersionIsAvailable()
        {
            if (MessageBox.Show("A new version is available. Do you want to download the new version of w3bot?", "New version available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Run();
            }
            else
            {
                Application.Exit();
            }
        }

        private void ExecuteApplication()
        {
            try
            {
                statusLabelMessage.Text = "Starting w3bot...";
                Process.Start(String.Format(@"{0}\{1}", _installPath, APPLICATION_NAME));
                Application.Exit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Run()
        {
            if (LoadingBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                LoadingBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}
