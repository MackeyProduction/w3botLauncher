using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public abstract class AbstractHttpClient
    {
        protected FileProcess FileProcess { get; private set; }
        protected string DestinationPath { get; private set; }
        protected string FileStatus { get; private set; }
        protected static bool IsFinished { get; private set; } = false;
        protected bool IsCancelled { get; private set; } = false;
        protected static bool Running { get; private set; } = false;
        private WebClient _webClient;

        public AbstractHttpClient(WebClient webClient, FileProcess fileProcess, string path)
        {
            _webClient = webClient;
            FileProcess = fileProcess;
            DestinationPath = path;
        }

        protected void Reset()
        {
            IsFinished = false;
            Running = false;
        }

        protected void Download(string fileName, string filePath)
        {
            try
            {
                if (FileExists(filePath))
                {
                    IsFinished = true;
                    return;
                }

                Task.Run(() => 
                {
                    _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                    _webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                    if (!_webClient.IsBusy)
                    {
                        _webClient.DownloadFileAsync(new Uri(Connection.ENDPOINT + fileName), filePath);
                    }
                    Running = true;
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected bool FileExists(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            if (File.Exists(fileName))
            {
                if (fileInfo.Length > 0)
                    return true;
            }

            return false;
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            IsFinished = !e.Cancelled;
            Running = false;
            if (e.Cancelled)
            {
                FileStatus = e.Error.Message;
                IsCancelled = true;
            }
            FileStatus += Environment.NewLine + "Download finished!";
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            FileProcess.ProgressPercentage = e.ProgressPercentage;
            FileProcess.BytesReceived = e.BytesReceived;
            FileProcess.TotalBytesToReceive = e.TotalBytesToReceive;
        }

        public string GetFullPath(string sourcePath, string destinationPath)
        {
            return String.Format(@"{0}\{1}", sourcePath, destinationPath);
        }
    }
}
