using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Security;

namespace DumbSearch
{
    public delegate void SearchStartedHandler(EventArgs e);
    public delegate void SearchFinishedHandler(EventArgs e);

    public delegate void FolderDiscoveryStartedHandler(EventArgs e);
    public delegate void FoldersDiscoveredHandler(FoldersFoundEventArgs e);
    public delegate void FolderDiscoveryFinishedHandler(FoldersFoundEventArgs e);

    public delegate void FileDiscoveryStartedHandler(EventArgs e);
    public delegate void FilesNameMatchingFoundHandler(FilesFoundEventArgs e);
    public delegate void FileDiscoveryFinishedHandler(FilesFoundEventArgs e);

    public delegate void FileContentDiscoveryStartedHandler(EventArgs e);
    public delegate void FileContentMatchFoundHandler(FileFoundEventArgs e);
    public delegate void FileContentDiscoveryFinishedHandler(EventArgs e);
    
    public enum SearchProgressStatus
    {
        DoingNothing,
        FolderDiscovery,
        FileDiscovery,
        ContentDiscovery,
        Finished
    }
    public struct SearcherProgress
    {
        public SearchProgressStatus CurrentStatus;
        public string WhatsHappenning;
        public int FoldersTotal;
        public int FoldersCurrent;
        public int FilesNamingMatchTotal;
        public int FilesCurrent;
        public int FilesContentTotal;
    }

    class Searcher
    {
        private object locker = new object();
        private SearchProgressStatus _searchProgressStatus = SearchProgressStatus.DoingNothing;
        private string _whatsHappenning;
        private List<DirectoryInfo> _folders;
        private int _foldersCurrent;
        private List<FileInfo> _fileNameMatch;
        private int _filesCurrent;
        private List<FileInfo> _fileContentMatch;

        public event SearchStartedHandler SearchStarted;
        public event SearchFinishedHandler SearchFinished;

        public event FolderDiscoveryStartedHandler FolderDiscoveryStarted;
        public event FoldersDiscoveredHandler FoldersDiscovered;
        public event FolderDiscoveryFinishedHandler FolderDiscoveryFinished;

        public event FileDiscoveryStartedHandler FileDiscoveryStarted;
        public event FilesNameMatchingFoundHandler FilesNameMatchingFound;
        public event FileDiscoveryFinishedHandler FileDiscoveryFinished;

        public event FileContentDiscoveryStartedHandler FileContentDiscoveryStarted;
        public event FileContentMatchFoundHandler FileContentMatchingFound;
        public event FileContentDiscoveryFinishedHandler FileContentDiscoveryFinished;


        public Searcher()
        {
            init();
        }

        private void init()
        {
            _searchProgressStatus = SearchProgressStatus.DoingNothing;

            _folders = new List<DirectoryInfo>();
            _foldersCurrent = 0;
            
            _filesCurrent = 0;
            _fileNameMatch = new List<FileInfo>();
            _fileContentMatch = new List<FileInfo>();

        }

        public SearcherProgress Status()
        {
            SearcherProgress status = new SearcherProgress();
            lock (locker)
            {
                status.CurrentStatus = _searchProgressStatus;
                status.WhatsHappenning = _whatsHappenning;
                
                status.FoldersTotal = _folders.Count;
                status.FoldersCurrent = _foldersCurrent;

                status.FilesNamingMatchTotal = _fileNameMatch.Count;
                status.FilesCurrent = _filesCurrent;
                status.FilesContentTotal = _fileContentMatch.Count;
            }
            return status;
        }

        public List<FileInfo> Search(DirectoryInfo rootFolder)
        {
            return Search(rootFolder, string.Empty, string.Empty);
        }
        public List<FileInfo> Search(DirectoryInfo rootFolder, string filenameMatchPattern)
        {
            return Search(rootFolder, filenameMatchPattern, string.Empty);
        }
        public List<FileInfo> Search(DirectoryInfo rootFolder, string filenameMatchPattern,string contentMatchPattern)
        {
            bool findContentInFiles = (contentMatchPattern != string.Empty) && (contentMatchPattern.Length>0) ;
            _whatsHappenning = "Initializing";

            init();

            if (SearchStarted != null)
                SearchStarted(new EventArgs());

            discoverFolders(rootFolder);

            discoverFiles(filenameMatchPattern);

            if (findContentInFiles)
                discoverContent(contentMatchPattern);

            if (SearchFinished != null)
                SearchFinished(new EventArgs());

            _searchProgressStatus = SearchProgressStatus.Finished;

            _whatsHappenning = "Terminating";

            if (findContentInFiles)
                return _fileContentMatch;
            else
                return _fileNameMatch ;
        }

        private void discoverFolders(DirectoryInfo rootFolder)
        {
            _searchProgressStatus = SearchProgressStatus.FolderDiscovery;
            Queue<DirectoryInfo> folderQueue = new Queue<DirectoryInfo>();

            if (FolderDiscoveryStarted != null)
                FolderDiscoveryStarted(new EventArgs());

            folderQueue.Enqueue(rootFolder);

            while (folderQueue.Count > 0)
            {
                DirectoryInfo folder = folderQueue.Dequeue();
                _whatsHappenning = folder.FullName;

                DirectoryInfo[] folders = null;

                try
                {
                    folders = folder.GetDirectories();
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (folders != null && folders.Length > 0)
                {
                    lock(locker)
                        _folders.AddRange(folders);

                    if (FoldersDiscovered != null)
                        FoldersDiscovered(new FoldersFoundEventArgs(new List<DirectoryInfo>(folders)));

                    foreach (DirectoryInfo subFolder in folders)
                    {
                        folderQueue.Enqueue(subFolder);
                    }
                }

            }

            if (FolderDiscoveryFinished != null)
                FolderDiscoveryFinished(new FoldersFoundEventArgs(_folders));

            
        }

        public void discoverFiles(string filenameMatchPattern)
        {
            _searchProgressStatus = SearchProgressStatus.FileDiscovery;

            bool searchWithFileNamePattern = filenameMatchPattern != string.Empty;

            if (FileDiscoveryStarted != null)
                FileDiscoveryStarted(new EventArgs());

            foreach (DirectoryInfo folder in _folders)
            {
                _whatsHappenning = string.Format("Discovering files: {0}", folder.FullName);

                FileInfo[] files = null;

                try
                {
                    if (searchWithFileNamePattern)
                        files = folder.GetFiles(filenameMatchPattern, SearchOption.TopDirectoryOnly);
                    else
                        files = folder.GetFiles();
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (files != null && files.Length > 0)
                {
                    lock(locker)
                        _fileNameMatch.AddRange(files);

                    if (FilesNameMatchingFound != null)
                        FilesNameMatchingFound(new FilesFoundEventArgs(new List<FileInfo>(files)));
                    
                }

                _foldersCurrent++;   

            }
            if (FileDiscoveryFinished != null)
                FileDiscoveryFinished(new FilesFoundEventArgs(_fileNameMatch));

        }


        private void discoverContent(string contentMatchPattern)
        {
            _searchProgressStatus = SearchProgressStatus.ContentDiscovery;
            if (FileContentDiscoveryStarted != null)
                FileContentDiscoveryStarted(new EventArgs());
                        
            foreach (FileInfo file in _fileNameMatch)
            {
                _whatsHappenning = string.Format("Discovering content within {0}", file.FullName);
                if (contentFoundInFile(file, contentMatchPattern))
                {
                    lock(locker)
                        _fileContentMatch.Add(file);

                    if (FileContentMatchingFound != null)
                        FileContentMatchingFound(new FileFoundEventArgs(file));
                }

                _filesCurrent++;
            }

            if (FileContentDiscoveryFinished != null)
                FileContentDiscoveryFinished(new EventArgs());            
        }

        private bool contentFoundInFile(FileInfo file, string contentPattern)
        {
            bool returnValue = false;
            Regex reggy = new Regex(contentPattern,RegexOptions.Multiline | RegexOptions.IgnoreCase);
            try
            {
                using (StreamReader reader = file.OpenText())
                {
                    string content = reader.ReadToEnd();

                    returnValue = reggy.IsMatch(content);
                }
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnValue;

        }
                
    }
}
