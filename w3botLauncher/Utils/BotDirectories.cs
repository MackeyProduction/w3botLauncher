using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Utils
{
    public static class BotDirectories
    {
        internal static string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\w3bot";
        internal static string binDir = baseDir + @"\bin";
        internal static string compiledDir = baseDir + @"\compiled";
        internal static string sourceDir = baseDir + @"\src";
        internal static bool IsHandled { get; set; } = false;

        internal static void CreateDirs()
        {
            Create(new string[] { baseDir, binDir, compiledDir, sourceDir });
        }

        private static void Create(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (!Directory.Exists(args[i]))
                    Directory.CreateDirectory(args[i]);
                else
                    IsHandled = true;
            }
        }
    }
}
