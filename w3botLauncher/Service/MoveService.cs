using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Command;
using w3botLauncher.Service.Factory;
using w3botLauncher.Utils;

namespace w3botLauncher.Service
{
    public class MoveService
    {
        private MoveFactory _moveFactory;

        public MoveService(MoveFactory moveFactory)
        {
            _moveFactory = moveFactory;
        }

        public ICommand Create(FileType fileType, string path)
        {
            return _moveFactory.Create(fileType, path);
        }
    }
}
