using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DumbSearch.Services
{
    public sealed class SearchService : DumbSearch.Services.ISearchService
    {
        private readonly Services.IFileSystem _fileSystemService;

        private DirectoryInfo _root;
        private Model.SearchParameters _parameters;

        private bool _folderNameIsFiltered = false;
        private bool _fileNameIsFiltered = false;
        private bool _fileContentIsFiltered = false;

        //private string _folderNameSearchPattern = string.Empty;
        //private string _fileNameSearchPattern = string.Empty;

        private Messages.CurrentProgress _progress;
        private Messages.ThereIsProgress _progressMessage;

        private IList<FileInfo> _foundFiles;

        private System.Text.RegularExpressions.Regex _regexForMatchingFilename;
        private System.Text.RegularExpressions.Regex _regexForMatchingFoldername;
        private System.Text.RegularExpressions.Regex _regexForMatchingFileContent;

        public SearchService(Services.IFileSystem fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }


        private void init(Model.SearchParameters parameters)
        {
            _parameters = parameters;
            _foundFiles = new List<FileInfo>();
            _progress = new Messages.CurrentProgress();
            _progressMessage = new Messages.ThereIsProgress(_progress);

            _folderNameIsFiltered = !string.IsNullOrEmpty(_parameters.Folder);
            _fileNameIsFiltered = !string.IsNullOrEmpty(_parameters.FileName);
            _fileContentIsFiltered = !string.IsNullOrEmpty(_parameters.Content);

            var regexOptions =
                        System.Text.RegularExpressions.RegexOptions.Compiled
                        | System.Text.RegularExpressions.RegexOptions.CultureInvariant
                        | System.Text.RegularExpressions.RegexOptions.IgnoreCase
                        | System.Text.RegularExpressions.RegexOptions.Singleline;

            if (_folderNameIsFiltered)
            {
                if (parameters.FolderIsRegex)
                    _regexForMatchingFoldername = new System.Text.RegularExpressions.Regex(_parameters.Folder,regexOptions);
                else
                {
                    var pattern = getSearchPattern(_parameters.Folder);
                    _regexForMatchingFoldername = new Wildcard(pattern, regexOptions);
                }
            }

            if (_fileNameIsFiltered)
            {
                if (parameters.FileNameIsRegex)
                    _regexForMatchingFilename = new System.Text.RegularExpressions.Regex(_parameters.FileName, regexOptions);
                else
                {
                    var pattern = getSearchPattern(_parameters.FileName);
                    _regexForMatchingFilename = new Wildcard(pattern, regexOptions);
                }
            }



            if (parameters.ContentIsRegex)
                _regexForMatchingFileContent = new System.Text.RegularExpressions.Regex(_parameters.Content);

            _root = new DirectoryInfo(_parameters.Root);
        }

        private string getSearchPattern(string filename)
        {
            if (filename.IndexOfAny(new char[] { '*', '?' }) == -1)
                return string.Concat("*", filename, "*");
            else
                return filename;
        }

        private void parseFolder(DirectoryInfo someFolder, bool folderIsMatched)
        {
            parseFilesInFolder(someFolder, folderIsMatched);

            parseSubFolders(someFolder, folderIsMatched);

            _progress.FoldersSurveyed++;

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.ThereIsProgress>(_progressMessage);

        }

        private void parseSubFolders(DirectoryInfo someFolder, bool folderIsMatched)
        {
            /* whether we filter or not folders, we need all of the sub folders -- it's for the count of folders */
            System.IO.DirectoryInfo[] subFolders = null;
            try
            {
                subFolders = _fileSystemService.GetSubFolders(someFolder);

            }
            catch (System.UnauthorizedAccessException)
            {

            }

            if (subFolders != null)
            {
                _progress.FoldersDiscovered += subFolders.Length;

                if (folderIsMatched)
                {
                    // we're matched, no need to check anything... just parse that subfolder.
                    foreach (var subFolder in subFolders)
                        parseFolder(subFolder, true);
                }
                else
                {
                    var filteredFolders = filterFolders(subFolders);
                    _progress.FoldersMatched += filteredFolders.Count;

                    foreach (var subFolder in subFolders)
                    {
                        var subFolderMatched = filteredFolders.Contains(subFolder.Name);
                        parseFolder(subFolder, subFolderMatched);
                    }

                }
            }

        }

        private void parseFilesInFolder(DirectoryInfo someFolder, bool folderIsMatched)
        {
            /* Do we need to find files in this folder ?
             * Always: unless We're filtering on FolderNames and this folder doesn't not match the request
             */

            var weNeedToFindFilesInThisFolder = !_folderNameIsFiltered || folderIsMatched;

            if (weNeedToFindFilesInThisFolder)
            {
                /* whether we filter or not files, we need all of the files in the folder -- it's for the count of files */
                var files = _fileSystemService.GetFilesInFolder(someFolder);
                _progress.FilesDiscovered += files.Length;

                var filteredFiles = filterFiles(files);
                processFiles(filteredFiles);
            }
        }

        private List<string> filterFolders(DirectoryInfo[] folders)
        {
            var filtered = new List<string>();

            if (_folderNameIsFiltered)
            {
                foreach (var folder in folders)
                {
                    if (_regexForMatchingFoldername.IsMatch(folder.Name))
                        filtered.Add(folder.Name);
                }
            }
            else
            {
                foreach (var folder in folders)
                    filtered.Add(folder.Name);
            }

            return filtered;
        }

        private FileInfo[] filterFiles(FileInfo[] files)
        {

            if (_fileNameIsFiltered)
            {
                var filtered = new List<FileInfo>();
                foreach (var file in files)
                {

                    if (_regexForMatchingFilename.IsMatch(file.Name))
                        filtered.Add(file);

                }

                return filtered.ToArray();
            }
            else
            {
                return files;
            }


        }

        private void processFiles(FileInfo[] files)
        {
            foreach (var file in files)
            {
                processFile(file);
            }
        }

        private void processFile(FileInfo file)
        {
            _progress.FilesMatched++;

            if (_fileContentIsFiltered)
            {
                processContent(file);
            }
            else
            {
                _progress.FilesSurveyed++;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.FileMatched>(new Messages.FileMatched(file));
            }
        }

        private void processContent(FileInfo file)
        {
            _progress.CurrentFile = file.FullName;
            _progress.FilesSurveyed++;

            if (file.Length < 30 * 1024 * 1024)
            {
                using (StreamReader sr = _fileSystemService.OpenFileAsText(file))
                {
                    var lineNumber = 0;
                    var line = string.Empty;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        lineNumber++;
                        int cent = (int)(100 * sr.BaseStream.Position / sr.BaseStream.Length);

                        if (cent != _progress.ContentMatchingProgress)
                        {
                            _progress.ContentMatchingProgress = cent;
                            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.ThereIsProgress>(_progressMessage);
                        }

                        if ((_parameters.ContentIsRegex && _regexForMatchingFileContent.IsMatch(line)) || line.Contains(_parameters.Content))
                        {
                            _foundFiles.Add(file);

                            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.ContentMatched>(
                                new Messages.ContentMatched(file)
                                {
                                    LineNumber = lineNumber
                                });

                            return;
                        }

                    }
                }

            }

        }


        #region ISearchService Members

        void ISearchService.Search(Model.SearchParameters parameters)
        {
            init(parameters);

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.SearchStarted>(new Messages.SearchStarted("Starting...  Can you here the gears?"));
            parseFolder(_root, false);

        }

        #endregion
    }
}
