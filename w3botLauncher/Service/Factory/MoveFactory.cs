using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Command;
using w3botLauncher.ExceptionMessage;
using w3botLauncher.Utils;

namespace w3botLauncher.Service.Factory
{
    public class MoveFactory
    {
        public MoveFactory()
        {
        }

        internal ICommand Create(FileType fileType, string path)
        {
            switch (fileType)
            {
                case FileType.Tesseract:
                    return new MoveTesseractFilesCommand(path);
                case FileType.Client:
                    return new MoveClientCommand(path);
                default:
                    break;
            }

            throw new InvalidFileTypeException(fileType.ToString());
        }
    }
}
