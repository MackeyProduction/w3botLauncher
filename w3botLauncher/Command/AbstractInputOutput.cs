using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public abstract class AbstractInputOutput
    {
        protected string DestinationPath { get; private set; }
        protected string CurrentPath { get; private set; }
        protected bool IsFinished { get; private set; } = false;

        public AbstractInputOutput(string path)
        {
            DestinationPath = path;
            CurrentPath = Directory.GetCurrentDirectory();
        }

        public void Move(string sourcePath, string destinationPath)
        {
            var sourceDirectory = new DirectoryInfo(sourcePath);
            var destinationDirectory = new DirectoryInfo(destinationPath);

            if (!sourceDirectory.Exists)
                throw new InvalidOperationException(String.Format("The directory by the name {0} does not exists.", sourcePath));

            if (!destinationDirectory.Exists)
                Directory.CreateDirectory(destinationPath);

            Task.Run(() =>
            {
                MoveDirectories(sourceDirectory, sourcePath, destinationPath);
                IsFinished = true;
            });

            while (!IsFinished)
                Thread.Sleep(100);
        }

        public void Remove(string destinationPath)
        {
            var destinationDirectory = new DirectoryInfo(destinationPath);

            Task.Run(() =>
            {
                if (destinationDirectory.GetFiles().Length > 0)
                    RemoveFiles(destinationDirectory, destinationPath);

                if (destinationDirectory.GetDirectories().Length > 0)
                    RemoveDirectories(destinationDirectory, destinationPath);

                Directory.Delete(destinationPath);
                IsFinished = true;
            });

            while (!IsFinished)
                Thread.Sleep(100);
        }

        private void MoveDirectories(DirectoryInfo sourceDirectory, string sourcePath, string destinationPath)
        {
            foreach (var directory in sourceDirectory.GetDirectories())
            {
                var newDirectory = new DirectoryInfo(GetFullPath(destinationPath, directory.Name));
                if (!newDirectory.Exists)
                    Directory.CreateDirectory(newDirectory.FullName);

                if (directory.Exists)
                    MoveDirectories(directory, directory.FullName, newDirectory.FullName);

                if (directory.GetFiles().Length > 0)
                    MoveFiles(directory, directory.FullName, newDirectory.FullName);

                MoveFiles(sourceDirectory, sourcePath, destinationPath);
            }
        }

        private void MoveFiles(DirectoryInfo directory, string sourcePath, string destinationPath)
        {
            foreach (var file in directory.GetFiles())
            {
                var fileName = file.Name;
                var currentPath = GetFullPath(sourcePath, fileName);
                var movePath = GetFullPath(destinationPath, fileName);

                File.Copy(currentPath, movePath, true);
            }
        }

        private void RemoveDirectories(DirectoryInfo destinationDirectory, string destinationPath)
        {
            foreach (var directory in destinationDirectory.GetDirectories())
            {
                if (directory.Exists)
                    RemoveDirectories(directory, directory.FullName);

                if (directory.GetFiles().Length > 0)
                    RemoveFiles(directory, directory.FullName);

                Directory.Delete(directory.FullName);
            }
        }

        private void RemoveFiles(DirectoryInfo destinationDirectory, string destinationPath)
        {
            foreach (var file in destinationDirectory.GetFiles())
            {
                var fileName = file.Name;
                var path = GetFullPath(destinationPath, fileName);

                File.Delete(path);
            }
        }

        public string GetFullPath(string sourcePath, string destinationPath)
        {
            return String.Format(@"{0}\{1}", sourcePath, destinationPath);
        }
    }
}
