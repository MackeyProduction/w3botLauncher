using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Command;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public interface IFileCommand : ICommand
    {
        FileProcess Process { get; }
    }
}
