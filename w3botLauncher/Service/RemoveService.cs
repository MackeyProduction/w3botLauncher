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
    public class RemoveService
    {
        private RemoveFactory _removeFactory;

        public RemoveService(RemoveFactory removeFactory)
        {
            _removeFactory = removeFactory;
        }

        public ICommand Create(FileType fileType, string path)
        {
            return _removeFactory.Create(fileType, path);
        }
    }
}
