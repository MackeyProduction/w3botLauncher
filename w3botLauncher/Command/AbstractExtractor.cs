using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using w3botLauncher.Utils;

namespace w3botLauncher.Command
{
    public abstract class AbstractExtractor
    {
        protected bool IsFinished { get; private set; } = false;
        protected bool Running { get; private set; } = false;

        public AbstractExtractor()
        {
        }

        public void Compress(string folder, string targetFileName)
        {
            try
            {
                ZipFile.CreateFromDirectory(folder, targetFileName, CompressionLevel.NoCompression, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Decompress(string sourcePath, string destinationPath)
        {
            try
            {
                if (!File.Exists(sourcePath))
                {
                    throw new InvalidOperationException(String.Format("The file by the name {0} could not be found.", sourcePath));
                }

                var fileInfo = new FileInfo(sourcePath);
                if (!fileInfo.Name.EndsWith(".zip"))
                {
                    throw new InvalidOperationException(String.Format("The file by the name {0} is'nt a zip archive.", sourcePath));
                }

                var extractPath = Regex.Replace(fileInfo.Name, ".zip", "");
                var destinationDirectory = String.Format(@"{0}\{1}", destinationPath, extractPath);
                if (Directory.Exists(destinationDirectory))
                    return;

                Task.Run(() =>
                {
                    ZipFile.ExtractToDirectory(sourcePath, destinationPath);
                    IsFinished = true;
                });

                while (!IsFinished)
                    Thread.Sleep(100);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
