using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using w3botLauncher.Service;
using w3botLauncher.Utils;

namespace w3bot.Command
{
    public class DownloadTesseractFilesCommand : AbstractHttpClient, ICommand
    {
        public DownloadTesseractFilesCommand(WebClient webClient, FileProcess fileProcess) : base(webClient, fileProcess)
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

        protected string FILE_PATH = BotDirectories.binDir;
        protected string FILE_NAME = "tessdata.zip";

        public void Execute()
        {
            try
            {
                Status = "Downloading tessdata files...";

                Download(FILE_PATH, FILE_NAME);

                if (Running)
                    IsRunning = Running;

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