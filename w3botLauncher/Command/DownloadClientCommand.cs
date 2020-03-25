using System;
using System.IO;
using System.Net;
using System.Threading;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class DownloadClientCommand : AbstractHttpClient, IFileCommand
    {
        public DownloadClientCommand(WebClient webClient, FileProcess fileProcess, string path) : base(webClient, fileProcess, path)
        {
        }

        public string Status { get; set; }
        public FileProcess Process
        {
            get
            {
                return FileProcess;
            }
        }

        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        private string FILE_NAME = "client.zip";

        public void Execute()
        {
            try
            {
                Status = "Downloading client...";

                IsRunning = Running;
                if (IsRunning)
                    return;

                if (IsFinished)
                {
                    IsHandled = true;
                    Reset();
                    return;
                }

                Download(FILE_NAME, GetFullPath(DestinationPath, FILE_NAME));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}