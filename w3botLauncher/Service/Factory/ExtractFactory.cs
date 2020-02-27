using System;
using w3botLauncher.Command;
using w3botLauncher.ExceptionMessage;
using w3botLauncher.Utils;

namespace w3botLauncher.Service.Factory
{
    public class ExtractFactory
    {
        public ExtractFactory()
        {
        }

        public ICommand Create(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Tesseract:
                    return new ExtractTesseractFilesCommand();
                case FileType.Client:
                    return new ExtractClientCommand();
                default:
                    break;
            }

            throw new InvalidFileTypeException(fileType.ToString());
        }
    }
}