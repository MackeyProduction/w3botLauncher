using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Utils
{
    public class FileProcess
    {
        public int ProgressPercentage { get; set; } = 0;
        public long BytesReceived { get; set; } = 0;
        public long TotalBytesToReceive { get; set; } = 0;
    }
}
