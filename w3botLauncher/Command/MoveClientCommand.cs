using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public class MoveClientCommand : AbstractInputOutput, ICommand
    {
        public MoveClientCommand(string path) : base(path)
        {
        }

        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }
        private const string MOVE_DIRECTORY = "client";

        public void Execute()
        {
            try
            {
                Status = "Moving client...";

                if (IsFinished)
                    IsHandled = true;

                Move(GetFullPath(CurrentPath, MOVE_DIRECTORY), DestinationPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
