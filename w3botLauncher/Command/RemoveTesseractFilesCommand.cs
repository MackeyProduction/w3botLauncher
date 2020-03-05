﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string TESSERACT_DIRECTORY = Directory.GetCurrentDirectory() + @"\tessdata";
        private string TESSERACT_FILE = Directory.GetCurrentDirectory() + @"\tessdata.zip";

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

                if (File.Exists(TESSERACT_FILE))
                    File.Delete(TESSERACT_FILE);
                Remove(TESSERACT_DIRECTORY);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
