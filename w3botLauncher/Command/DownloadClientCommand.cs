using System;
using System.IO;
using System.Net;
using System.Threading;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class DownloadClientCommand : AbstractHttpClient, IFileCommand
    {
        public DownloadClientCommand(WebClient webClient, FileProcess fileProcess) : base(webClient, fileProcess)
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

        private string FILE_PATH = BotDirectories.baseDir;
        private string FILE_NAME = "client.zip";

        public void Execute()
        {
            try
            {
                Status = "Downloading client...";

                Download(FILE_NAME);

                IsRunning = Running;

                if (IsFinished || FileExists(FILE_NAME))
                    IsHandled = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}