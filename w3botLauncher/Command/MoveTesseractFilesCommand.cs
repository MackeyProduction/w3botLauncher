using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public class MoveTesseractFilesCommand : ICommand
    {
        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        public void Execute()
        {
            
        }
    }
}
