using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class RemoveTesseractFilesCommand : AbstractInputOutput, ICommand
    {
        public RemoveTesseractFilesCommand(string path) : base(path)
        {
        }

        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        private string TESSERACT_FILE = BotDirectories.binDir + @"\tessdata.zip";

        public void Execute()
        {
            try
            {
                Status = "Removing tesseract files...";

                if (IsFinished)
                {
                    IsHandled = true;
                    return;
                }

                RemoveFile(TESSERACT_FILE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
