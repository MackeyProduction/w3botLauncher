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
        protected const string ENDPOINT = "http://192.168.178.47:8000/w3bot/client/";
        protected FileProcess FileProcess { get; private set; }
        protected string FileStatus { get; private set; }
        protected static bool IsFinished { get; private set; }
        protected bool IsCancelled { get; private set; }
        protected static bool Running { get; private set; }
        private WebClient _webClient;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);

        public AbstractHttpClient(WebClient webClient, FileProcess fileProcess)
        {
            _webClient = webClient;
            FileProcess = fileProcess;
        }

        protected void Download(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    return;

                _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                _webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                _webClient.DownloadFileAsync(new Uri(ENDPOINT + fileName), fileName);
                _semaphore.Wait(100);
                Running = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this._semaphore.Dispose();
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
    }
}
