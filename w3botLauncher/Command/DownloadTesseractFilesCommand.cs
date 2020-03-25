using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class DownloadTesseractFilesCommand : AbstractHttpClient, IFileCommand
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

                IsRunning = Running;
                if (IsRunning)
                    return;

                if (IsFinished)
                {
                    IsHandled = true;
                    Reset();
                    return;
                }

                Download(FILE_NAME, String.Format(@"{0}\{1}", FILE_PATH, FILE_NAME));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}