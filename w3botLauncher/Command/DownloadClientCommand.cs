using System;
using System.IO;
using System.Net;
using System.Threading;
using w3bot.Command;
using w3botLauncher.Service;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class DownloadClientCommand : AbstractHttpClient, ICommand
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
            set
            {
                Process = value;
            }
        }

        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        private string FILE_PATH = BotDirectories.baseDir;
        private string FILE_NAME = "w3bot.exe";

        public void Execute()
        {
            try
            {
                Status = "Downloading client...";

                Download(FILE_PATH, FILE_NAME);

                if (Running)
                    IsRunning = true;

                if (IsFinished || FileExists(FILE_PATH, FILE_NAME))
                    IsHandled = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}