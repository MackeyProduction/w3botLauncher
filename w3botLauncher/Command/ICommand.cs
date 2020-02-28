using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public interface ICommand
    {
        string Status { get; set; }
        bool IsHandled { get; set; }
        bool IsRunning { get; set; }
        void Execute();
    }
}
