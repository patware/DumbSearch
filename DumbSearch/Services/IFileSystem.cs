using System;
namespace DumbSearch.Services
{
    public interface IFileSystem
    {
        System.IO.FileInfo[] GetFilesInFolder(System.IO.DirectoryInfo folder);
        System.IO.DirectoryInfo[] GetSubFolders(System.IO.DirectoryInfo folder);
        System.IO.StreamReader OpenFileAsText(System.IO.FileInfo file);
    }
}
