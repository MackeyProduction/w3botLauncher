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

        private string SOURCE_PATH = BotDirectories.binDir + @"\tessdata.zip";
        private string DESTINATION_PATH = BotDirectories.binDir;

        public void Execute()
        {
            Status = "Extracting tesseract files...";

            if (IsFinished || Directory.Exists(DESTINATION_PATH))
                IsHandled = true;

            Decompress(SOURCE_PATH, DESTINATION_PATH);
        }
    }
}
