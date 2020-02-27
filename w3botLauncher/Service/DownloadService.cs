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
    public class DownloadService
    {
        private DownloadFactory _downloadFactory;

        public DownloadService(DownloadFactory factory)
        {
            _downloadFactory = factory;
        }

        public IFileCommand Create(FileType downloadType)
        {
            return _downloadFactory.Create(downloadType);
        }
    }
}
