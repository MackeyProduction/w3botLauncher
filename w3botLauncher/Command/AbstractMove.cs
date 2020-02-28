﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Command
{
    public abstract class AbstractMove
    {
        protected string MovePath { get; private set; }
        protected string CurrentPath { get; private set; }
        protected bool IsFinished { get; private set; }

        public AbstractMove(string path)
        {
            MovePath = path;
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

            if (destinationDirectory.GetFiles().Length > 0)
            {
                IsFinished = true;
                return;
            }

            foreach (var directory in sourceDirectory.GetDirectories())
            {
                var newDirectory = new DirectoryInfo(GetFullPath(destinationPath, directory.Name));
                if (!newDirectory.Exists)
                    Directory.CreateDirectory(newDirectory.FullName);

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

                File.Move(currentPath, movePath);
            }
        }

        public string GetFullPath(string sourcePath, string destinationPath)
        {
            return String.Format(@"{0}\{1}", sourcePath, destinationPath);
        }
    }
}
