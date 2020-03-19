using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class ExtractClientCommand : AbstractExtractor, ICommand
    {
        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        private string SOURCE_PATH = Directory.GetCurrentDirectory() + @"\client.zip";
        private string DESTINATION_PATH = Directory.GetCurrentDirectory();

        public void Execute()
        {
            Status = "Extracting client...";

            if (IsFinished || Directory.Exists(DESTINATION_PATH))
                IsHandled = true;

            Decompress(SOURCE_PATH, DESTINATION_PATH);
        }
    }
}
