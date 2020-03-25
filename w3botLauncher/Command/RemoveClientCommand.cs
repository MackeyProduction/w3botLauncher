using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

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

        private string CLIENT_FILE = RegistryUtils.GetRegistryEntry() + @"\client.zip";

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

                RemoveFile(CLIENT_FILE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
