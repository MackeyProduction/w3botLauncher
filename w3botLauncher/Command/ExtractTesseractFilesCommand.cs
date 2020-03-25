using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class ExtractTesseractFilesCommand : AbstractExtractor, ICommand
    {
        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; } = false;

        private const string FILE_NAME = "tessdata.zip";

        public ExtractTesseractFilesCommand(string path) : base(path)
        {
        }

        public void Execute()
        {
            Status = "Extracting tesseract files...";

            if (IsFinished || Directory.Exists(DestinationPath))
                IsHandled = true;

            Decompress(GetFullPath(DestinationPath, FILE_NAME), DestinationPath);
        }
    }
}
