using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public class RemoveClientCommand : AbstractInputOutput, ICommand
    {
        public RemoveClientCommand(string path) : base(path)
        {
        }

        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        private string CLIENT_DIRECTORY = Directory.GetCurrentDirectory() + @"\w3bot";
        private string CLIENT_FILE = Directory.GetCurrentDirectory() + @"\w3bot.zip";

        public void Execute()
        {
            try
            {
                Status = "Removing client...";

                if (IsFinished)
                {
                    IsHandled = true;
                    return;
                }

                if (File.Exists(CLIENT_FILE))
                    File.Delete(CLIENT_FILE);
                Remove(CLIENT_DIRECTORY);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
