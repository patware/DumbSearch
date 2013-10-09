using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DumbSearch.Services
{
    public sealed class FileSystem : DumbSearch.Services.IFileSystem
    {
        #region IFileSystem Members

        FileInfo[] IFileSystem.GetFilesInFolder(DirectoryInfo folder)
        {
            try
            {
                return folder.GetFiles();
            }
            catch (Exception)
            {
                return new FileInfo[0];
            }
            
        }

        DirectoryInfo[] IFileSystem.GetSubFolders(DirectoryInfo folder)
        {
            return folder.GetDirectories();
        }

        StreamReader IFileSystem.OpenFileAsText(FileInfo file)
        {
            return file.OpenText();
        }

        DirectoryInfo IFileSystem.AskUserForFolder(DirectoryInfo currentFolder)
        {
            var di = currentFolder;

            var fbd = new System.Windows.Forms.FolderBrowserDialog();

            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            if (currentFolder != null)
                fbd.SelectedPath = currentFolder.FullName;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                di = new DirectoryInfo(fbd.SelectedPath);
            }

            return di;
        }

        #endregion
    }
}
