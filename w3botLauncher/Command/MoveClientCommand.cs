using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public class MoveClientCommand : AbstractMove, ICommand
    {
        public MoveClientCommand(string path) : base(path)
        {
        }

        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }
        private const string MOVE_DIRECTORY = "w3bot";

        public void Execute()
        {
            Status = "Moving client...";
            
            if (IsFinished)
                IsHandled = true;

            Move(GetFullPath(CurrentPath, MOVE_DIRECTORY), MovePath);
        }
    }
}
