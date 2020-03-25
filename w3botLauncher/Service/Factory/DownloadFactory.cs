using System;
using System.Net;
using w3botLauncher.Command;
using w3botLauncher.ExceptionMessage;
using w3botLauncher.Utils;

namespace w3botLauncher.Service.Factory
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

        public IFileCommand Create(FileType fileType, string path)
        {
            switch (fileType)
            {
                case FileType.Tesseract:
                    return new DownloadTesseractFilesCommand(_webClient, _fileProcess, path);
                case FileType.Client:
                    return new DownloadClientCommand(_webClient, _fileProcess, path);
                default:
                    break;
            }

            throw new InvalidFileTypeException(fileType.ToString());
        }
    }
}