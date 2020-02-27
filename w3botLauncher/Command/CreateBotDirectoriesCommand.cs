using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public class CreateBotDirectoriesCommand : ICommand
    {
        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        public void Execute()
        {
            Status = "Creating directories...";

            IsHandled = BotDirectories.IsHandled;

            BotDirectories.CreateDirs();
        }
    }
}
