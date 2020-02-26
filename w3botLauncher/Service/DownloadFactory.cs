using System;
using System.Net;
using w3bot.Command;
using w3botLauncher.Command;
using w3botLauncher.Utils;

namespace w3botLauncher.Service
{
    public class DownloadFactory
    {
        private WebClient _webClient;
        private FileProcess _fileProcess;

        public DownloadFactory(WebClient webClient, FileProcess fileProcess)
        {
            _webClient = webClient;
            _fileProcess = fileProcess;
        }

        public ICommand Create(DownloadType downloadType)
        {
            switch (downloadType)
            {
                case DownloadType.Tesseract:
                    return new DownloadTesseractFilesCommand(_webClient, _fileProcess);
                case DownloadType.Client:
                    return new DownloadClientCommand(_webClient, _fileProcess);
                default:
                    break;
            }

            throw new InvalidOperationException(String.Format("The download type by the name {0} could not be found.", downloadType));
        }
    }
}