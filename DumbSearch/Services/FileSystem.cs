using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DumbSearch.Services
{
    public class FileSystem : DumbSearch.Services.IFileSystem
    {
        #region IFileSystem Members

        FileInfo[] IFileSystem.GetFilesInFolder(DirectoryInfo folder)
        {
            return folder.GetFiles();
        }

        DirectoryInfo[] IFileSystem.GetSubFolders(DirectoryInfo folder)
        {
            return folder.GetDirectories();
        }

        StreamReader IFileSystem.OpenFileAsText(FileInfo file)
        {
            return file.OpenText();
        }

        #endregion
    }
}
