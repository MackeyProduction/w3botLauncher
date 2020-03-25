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
    public class ExtractService
    {
        private ExtractFactory _extractFactory;

        public ExtractService(ExtractFactory extractFactory)
        {
            _extractFactory = extractFactory;
        }

        public ICommand Create(FileType fileType, string path)
        {
            return _extractFactory.Create(fileType, path);
        }
    }
}
