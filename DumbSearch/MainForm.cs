using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace DumbSearch
{
    public partial class MainForm : Form
    {
        private SearchStatus _currentSearchStatus = new SearchStatus();
        private BindingList<FileInfo> _matches = new BindingList<FileInfo>();
        private struct SearchStatus
        {
            public int OverallStep;
            public int FolderDiscoveryValue;
            public int FileDiscoveryValue;
            public string WhatsHappenning;
            public int FileCount;
            public Queue<FileInfo> FileMatches;
        }

        private Searcher _searcher;

        private struct BackgroundWorkerParameter
        {
            public DirectoryInfo RootFolder;
            public string FilenameMatchPattern;
            public string ContentMatchPattern;
        }

        public MainForm()
        {
            
            InitializeComponent();
        }

        private void uxSearchButton_Click(object sender, EventArgs e)
        {
            
            if (uxSearchTermTextBox.Text.Length > 0)
            {
                try
                {
                    Regex reggy = new Regex(uxSearchTermTextBox.Text, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Content Pattern error: " + ex.Message);
                    return;
                }
            }
            _matches = new BindingList<FileInfo>();
            uxFilesFoundListBox.DataSource = null;
            uxFilesFoundListBox.DataSource = _matches;

            _currentSearchStatus = new SearchStatus();
            _currentSearchStatus.FileMatches = new Queue<FileInfo>();

            timer1.Start();
            
            BackgroundWorkerParameter p = new BackgroundWorkerParameter();
            p.RootFolder = new DirectoryInfo(uxRootFolderTextBox.Text);
            p.FilenameMatchPattern = uxFilePatternTextbox.Text;
            p.ContentMatchPattern = uxSearchTermTextBox.Text;

            //doSearch(p);
            uxSearchBackgroundWorker.RunWorkerAsync(p);
            
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            _currentSearchStatus.FileMatches = new Queue<FileInfo>();
        }

        void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void uxRootFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (uxRootFolderTextBox.Focused)
            {
                Properties.Settings.Default.RootFolder = uxRootFolderTextBox.Text;
                Properties.Settings.Default.Save();
            }
        }
        private void uxFilePatternTextbox_TextChanged(object sender, EventArgs e)
        {
            if (uxFilePatternTextbox.Focused)
            {
                Properties.Settings.Default.SearchPattern = uxFilePatternTextbox.Text;
                Properties.Settings.Default.Save();
            }
        }
        private void uxSearchTermTextBox_TextChanged(object sender, EventArgs e)
        {
            if (uxSearchTermTextBox.Focused)
            {
                Properties.Settings.Default.SearchTerm = uxSearchTermTextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void uxPauseButton_Click(object sender, EventArgs e)
        {

                
        }

        private void uxStopButton_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (uxSearchBackgroundWorker.IsBusy)
            {
                showProgress();
            }
        }

        private void showProgress()
        {
            if (_searcher == null)
            {
                uxOveralProgressBar.Value = 0;
                uxFolderDiscoveryProgressBar.Value = 0;
                uxFileDiscoveryProgressBar.Value = 0;
                uxFileCounLabel.Text = "0";
                uxFileMatchLabel.Text = "0";
            }
            else
            {
                SearcherProgress progress = _searcher.Status();
                uxWhatsHappenningLabel.Text = progress.WhatsHappenning;

                switch (progress.CurrentStatus)
                {
                    case SearchProgressStatus.DoingNothing:
                        uxOveralProgressBar.Value = 0;
                        uxFolderDiscoveryProgressBar.Value = 0;
                        uxFileDiscoveryProgressBar.Value = 0;
                        uxFileCounLabel.Text = "0";
                        uxFileMatchLabel.Text = "0";
                        break;
                    case SearchProgressStatus.FolderDiscovery:
                        uxOveralProgressBar.Value = 1;
                        uxFolderDiscoveryProgressBar.Value = 0;
                        uxFileDiscoveryProgressBar.Value = 0;
                        uxFileCounLabel.Text = "0";
                        uxFileMatchLabel.Text = "0";
                        break;
                    case SearchProgressStatus.FileDiscovery:
                        uxOveralProgressBar.Value = 2;
                        if (progress.FoldersTotal > 0)
                            uxFolderDiscoveryProgressBar.Value = (int)(100.0 * progress.FoldersCurrent / progress.FoldersTotal);
                        else
                            uxFolderDiscoveryProgressBar.Value = 0;
                        uxFileDiscoveryProgressBar.Value = 0;
                        uxFileCounLabel.Text = progress.FilesNamingMatchTotal.ToString();
                        uxFileMatchLabel.Text = "0";
                        break;
                    case SearchProgressStatus.ContentDiscovery:
                        uxOveralProgressBar.Value = 3;
                        uxFolderDiscoveryProgressBar.Value = 100;
                        if (progress.FilesNamingMatchTotal > 0)
                            uxFileDiscoveryProgressBar.Value = (int)(100.0 * progress.FilesCurrent / progress.FilesNamingMatchTotal);
                        else
                            uxFileDiscoveryProgressBar.Value = 0;
                        uxFileCounLabel.Text = progress.FilesNamingMatchTotal.ToString();
                        uxFileMatchLabel.Text = progress.FilesContentTotal.ToString();
                        break;
                    case SearchProgressStatus.Finished:
                        uxOveralProgressBar.Value = 4;
                        uxFolderDiscoveryProgressBar.Value = 100;
                        uxFileDiscoveryProgressBar.Value = 100;
                        uxFileCounLabel.Text = progress.FilesNamingMatchTotal.ToString();
                        uxFileMatchLabel.Text = progress.FilesContentTotal.ToString() ;
                        break; 
                }
            }
        }


        private void uxFilesFoundListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uxFilesFoundListBox.SelectedItem != null)
                uxSelectedFileTextBox.Text = ((FileInfo)uxFilesFoundListBox.SelectedItem).FullName;
        }

        private void uxFilesFoundListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (uxFilesFoundListBox.SelectedItem != null)
            {
                TextViewerForm tvf = new TextViewerForm();
                tvf.Show();
                tvf.LoadFile((FileInfo)uxFilesFoundListBox.SelectedItem, uxSearchTermTextBox.Text);
            }
        }

        private void uxClearLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _matches.Clear();
        }

        private void uxClipboardLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //StringBuilder sb = new StringBuilder();

            //foreach (FileInfo file in _files)
            //{
            //    sb.AppendLine(file.FullName);
            //}

            //System.Windows.Forms.Clipboard.SetText(sb.ToString(),TextDataFormat.UnicodeText);
        }



        private void uxSearchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorkerParameter param = (BackgroundWorkerParameter)e.Argument;
                        
            e.Result = doSearch(param);
                                    
        }

        private List<FileInfo> doSearch(BackgroundWorkerParameter param)
        {
            _searcher = new Searcher();
            if (param.ContentMatchPattern.Length > 0)
                _searcher.FileContentMatchingFound += new FileContentMatchFoundHandler(_searcher_FileContentMatchingFound);
            else
                _searcher.FilesNameMatchingFound += new FilesNameMatchingFoundHandler(_searcher_FilesNameMatchingFound);

            List<FileInfo> filesFound = _searcher.Search(param.RootFolder, param.FilenameMatchPattern, param.ContentMatchPattern);

            return filesFound;
        }

        private delegate void _searcher_FilesNameMatchingFoundDelegate(List<FileInfo> files);
        private void _searcher_FilesNameMatchingFound(List<FileInfo> files)
        {
            _matches.RaiseListChangedEvents = false;

            foreach (FileInfo file in files)
                _matches.Add(file);

            _matches.RaiseListChangedEvents = true;
            _matches.ResetBindings();
        }

        void _searcher_FilesNameMatchingFound(FilesFoundEventArgs e)
        {
            if (uxFilesFoundListBox.InvokeRequired)
                uxFilesFoundListBox.Invoke(new _searcher_FilesNameMatchingFoundDelegate(_searcher_FilesNameMatchingFound), e.Files);
            else
                _searcher_FilesNameMatchingFound(e.Files);
            
        }

        private delegate void _searcher_FileContentMatchingFoundDelegate(FileInfo file);
        private void _searcher_FileContentMatchingFound(FileInfo file)
        {
            _matches.Add(file);
        }

        void _searcher_FileContentMatchingFound(FileFoundEventArgs e)
        {
            if (uxFilesFoundListBox.InvokeRequired)
                uxFilesFoundListBox.Invoke(new _searcher_FileContentMatchingFoundDelegate(_searcher_FileContentMatchingFound), e.File);
            else
                _searcher_FileContentMatchingFound(e.File);

        }

        
        private List<FileInfo> doSearch1(BackgroundWorkerParameter param)
        {
            _currentSearchStatus.OverallStep = 1;
            List<DirectoryInfo> everyFolder = gatherEveryFolder(param.RootFolder);

            _currentSearchStatus.OverallStep = 2;
            List<FileInfo> everyFile = gatherEveryFile(everyFolder, param.FilenameMatchPattern, true);

            _currentSearchStatus.OverallStep = 3;
            if (param.ContentMatchPattern == string.Empty)
            {
                return everyFile;
            }
            else
            {
                List<FileInfo> contentFound = new List<FileInfo>();
                foreach (FileInfo file in everyFile)
                {
                    if (checkIfContentFoundInFile(file, param.ContentMatchPattern))
                    {
                        contentFound.Add(file);
                    }
                }

                return contentFound;
            }
            
        }

        private List<DirectoryInfo> gatherEveryFolder(DirectoryInfo rootFolder)
        {
            _currentSearchStatus.WhatsHappenning = "Gathering the list of folders";
            List<DirectoryInfo> folders = new List<DirectoryInfo>();

            Queue<DirectoryInfo> foldersToDiscover = new Queue<DirectoryInfo>();
            foldersToDiscover.Enqueue(rootFolder);

            while (foldersToDiscover.Count > 0)
            {
                DirectoryInfo folder = foldersToDiscover.Dequeue();
                folders.Add(folder);

                DirectoryInfo[] subFolders = folder.GetDirectories();

                foreach (DirectoryInfo subFolder in subFolders)
                    foldersToDiscover.Enqueue(subFolder);
            }

            _currentSearchStatus.FolderDiscoveryValue = 100;
            return folders;
        }

        private List<FileInfo> gatherEveryFile(List<DirectoryInfo> everyFolder, string filenamePattern, bool reportProgress)
        {
            _currentSearchStatus.WhatsHappenning = "Gathering the list of files";
            List<FileInfo> files = new List<FileInfo>();

            bool searchWithPattern = filenamePattern != string.Empty;
            int currentFolderIndex = 0;

            foreach (DirectoryInfo folder in everyFolder)
            {
                currentFolderIndex++;
                _currentSearchStatus.FolderDiscoveryValue = (int)((100.0 * currentFolderIndex) / everyFolder.Count);

                FileInfo[] filesFound;
                if (searchWithPattern)
                {
                    filesFound = folder.GetFiles(filenamePattern, SearchOption.TopDirectoryOnly);
                }
                else
                {
                    filesFound = folder.GetFiles();
                }

                _currentSearchStatus.FileCount = files.Count;

                files.AddRange(filesFound);

                if (reportProgress && filesFound.Length > 0)
                {
                    foreach (FileInfo file in files)
                    {
                        _currentSearchStatus.FileMatches.Enqueue(file);
                    }

                }
                Application.DoEvents();
                
            }

            return files;
        }

        private bool checkIfContentFoundInFile(FileInfo file, string pattern)
        {
            bool returnValue = false;
            Regex reggy = new Regex(pattern);
            using (StreamReader reader = file.OpenText())
            {
                string content = reader.ReadToEnd();

                returnValue = reggy.IsMatch(content);
            }
            return returnValue;
        }



        private void uxSearchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            uxWhatsHappenningLabel.Text = "Done!!!";
            timer1.Stop();
            showProgress();

            //uxWhatsHappenningLabel.Text = "Loading results";
            //Application.DoEvents();

            //List<FileInfo> files = (List<FileInfo>)e.Result;

            //_matches.RaiseListChangedEvents = false;

            //foreach (FileInfo file in files)
            //    _matches.Add(file);

            //_matches.RaiseListChangedEvents = true;
            //_matches.ResetBindings();
        }



    }
}