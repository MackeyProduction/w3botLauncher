using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public class MoveTesseractFilesCommand : AbstractMove, ICommand
    {
        public MoveTesseractFilesCommand(string path) : base(path)
        {
        }

        public string Status { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }
        private const string MOVE_DIRECTORY = "tessdata";

        public void Execute()
        {
            try
            {
                Status = "Moving tessdata files...";

                if (IsFinished)
                    IsHandled = true;

                Move(GetFullPath(CurrentPath, MOVE_DIRECTORY), GetFullPath(MovePath, MOVE_DIRECTORY));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
