using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DumbSearch.Services
{
    public class SearchHelper
    {
        private DirectoryInfo _root;
        private Model.SearchParameters _parameters;

        private bool _folderNameIsFiltered = false;
        private bool _fileNameIsFiltered = false;
        private bool _fileContentIsFiltered = false;

        private Messages.CurrentProgress _progress;
        private Messages.ThereIsProgress _progressMessage;
        
        private IList<FileInfo> _foundFiles;

        private System.Text.RegularExpressions.Regex _regexForMatchingFilename;
        private System.Text.RegularExpressions.Regex _regexForMatchingFolderName;
        private System.Text.RegularExpressions.Regex _regexForMatchingFileContent;
        
        public void Search(Model.SearchParameters parameters)
        {

            init(parameters);

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.SearchStarted>(new Messages.SearchStarted("Starting...  Can you here the gears?"));
            _progress.FoldersSearched = -1;
            parseFolder(_root, false);
                        
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

            if (parameters.FileNameIsRegex)
                _regexForMatchingFilename = new System.Text.RegularExpressions.Regex(_parameters.FileName);

            if (parameters.FolderIsRegex)
                _regexForMatchingFolderName = new System.Text.RegularExpressions.Regex(_parameters.Folder);

            if (parameters.ContentIsRegex)
                _regexForMatchingFileContent = new System.Text.RegularExpressions.Regex(_parameters.Content);

            _root = new DirectoryInfo(_parameters.Root);
        }

        private void parseFolder(DirectoryInfo someFolder, bool folderIsMatched)
        {

            /* whether we filter or not folders, we need all of the sub folders -- it's for the count of folders */
            var subFolders = someFolder.GetDirectories();
            _progress.FoldersFound += subFolders.Length;
            
            /* whether we filter or not files, we need all of the files in the folder -- it's for the count of files */
            var files = someFolder.GetFiles();
            _progress.FilesFound += files.Length;

            /* Do we need to find files in this folder ?
             * Always: unless We're filtering on FolderNames and this folder doesn't not match the request
             */

            var weNeedToFindFilesInThisFolder = !(_folderNameIsFiltered && !folderIsMatched);

            if (weNeedToFindFilesInThisFolder)
            {
                var filteredFiles = getFilteredFilesFromFolder(someFolder);
                processFiles(filteredFiles);
            }


            if (_folderNameIsFiltered)
            {
                var filteredFolders = getFilteredSubFoldersFromFolder(someFolder);
                _progress.FoldersMatched += filteredFolders.Count;

                foreach (var subFolder in subFolders)
                {
                    parseFolder(subFolder, folderIsMatched || filteredFolders.Contains(subFolder.Name));
                }
            }
            else
            {
                _progress.FoldersMatched += subFolders.Length;
                foreach (var subFolder in subFolders)
                {
                    parseFolder(subFolder, true);
                }
            }
            _progress.FoldersSearched++;

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.ThereIsProgress>(_progressMessage);
                       
        }

        private FileInfo[] getFilteredFilesFromFolder(DirectoryInfo someFolder)
        {
            FileInfo[] filteredFiles;
            
            if (_parameters.FileNameIsRegex)
            {
                IList<FileInfo> filtered = new List<FileInfo>();

                foreach (var file in someFolder.GetFiles())
                {

                    if (_regexForMatchingFilename.IsMatch(file.Name))
                        filtered.Add(file);

                }
                filteredFiles = filtered.ToArray();

            }

            if (_fileNameIsFiltered)
            {
                filteredFiles = someFolder.GetFiles(_parameters.FileName);
            }
            else
            {
                filteredFiles = someFolder.GetFiles();
            }

            return filteredFiles;
        }
        
        private List<string> getFilteredSubFoldersFromFolder(DirectoryInfo someFolder)
        {
            var filtered = new List<string>();

            if (_parameters.FolderIsRegex)
            {    
                foreach (var folder in someFolder.GetDirectories())
                {
                    if (_regexForMatchingFolderName.IsMatch(folder.Name))
                        filtered.Add(folder.Name);
                }
            }

            DirectoryInfo[] temp;

            if (_folderNameIsFiltered)
                temp = someFolder.GetDirectories(_parameters.Folder);
            else
                temp = someFolder.GetDirectories();

            foreach (var t in temp)
            {
                filtered.Add(t.Name);
            }

            return filtered;
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
                _progress.FilesSearched++;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.FileMatched>(new Messages.FileMatched(file));
            }
        }

        private void processContent(FileInfo file)
        {
            _progress.CurrentFile = file.FullName;
            _progress.FilesSearched++;

            if (file.Length < 30 * 1024 * 1024)
            {
                using (StreamReader sr = file.OpenText())
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

    }
}
