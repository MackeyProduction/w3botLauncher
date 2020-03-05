using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Utils
{
    public class VersionLoader : MarshalByRefObject
    {
        public Version Version { get; set; }

        public void Load(string file)
        {
            var clientAssembly = Assembly.LoadFile(file);
            Version = clientAssembly.GetName().Version;
        }
    }
}
