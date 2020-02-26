using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3bot.Command
{
    public class BotDirectories : ICommand
    {
        internal static string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\w3bot";
        internal static string binDir = baseDir + @"\bin";
        internal static string compiledDir = baseDir + @"\compiled";
        internal static string sourceDir = baseDir + @"\src";

        public string Status { get; set; }
        public FileProcess Process { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRunning { get; set; }

        internal void CreateDirs()
        {
            Create(new string[] { baseDir, binDir, compiledDir, sourceDir });
        }

        private void Create(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (!Directory.Exists(args[i]))
                    Directory.CreateDirectory(args[i]);
                else
                    IsHandled = true;
            }
        }

        public void Execute()
        {
            Status = "Creating directories...";

            CreateDirs();
        }
    }
}
