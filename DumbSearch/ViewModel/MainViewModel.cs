using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using DumbSearch.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Reflection;
using System.IO;
using System.Windows.Threading;

namespace DumbSearch.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });

            if (IsInDesignMode)
            {
                _applicationTitle = "DumbSearch x.x - By Patware";
                _content = "...Content...";
                _contentIsRegex = true;
                _contentMatchingProgress = 12;
                _contentMatchingProgressIsVisible = true;
                _dateCreatedIsChecked = true;
                _dateCreatedToIsVisible = true;
                _dateModifiedIsChecked = true;
                _dateModifiedToIsVisible = true;
                _file = "...File...";
                _fileIsRegex = true;
                _filesFound = "Files Found";
                _filesMatched = "Files Matched";
                _filesSearched = "Files Searched";
                _folder = "...Folder...";
                _folderIsRegex = true;
                _foldersFound = "Folders Found";
                _foldersMatched = "Folders Matched";
                _foldersSearched = "Folders Searched";
                _foundItems.Add("C:\\Foo");
                _root = "...Root...";
                _sizeFrom = 123;
                _sizeFromUnit = "Megabyte";
                _sizeIsChecked = true;
                _sizeTo = 234;
                _sizeToIsVisible = true;
                _sizeToUnit = "Gigabyte";
                _sizeToUnitIsVisible = true;

            }
            else
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                _applicationTitle = string.Format("DumbSearch {0}.{1} - by Patware", version.Major, version.Minor);

                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.SearchStarted>(this, doSearchStarted);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.ThereIsProgress>(this, doThereIsProgress);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.FileMatched>(this, doFileMatched);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.ContentMatched>(this, doContentMatched);

            }
        }



        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

        #region Properties

        #region ApplicationTitle
        /// <summary>
        /// The <see cref="ApplicationTitle" /> property's name.
        /// </summary>
        public const string ApplicationTitlePropertyName = "ApplicationTitle";

        private string _applicationTitle = string.Empty;

        /// <summary>
        /// Sets and gets the ApplicationTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ApplicationTitle
        {
            get
            {
                return _applicationTitle;
            }

            set
            {
                if (_applicationTitle == value)
                {
                    return;
                }

                RaisePropertyChanging(ApplicationTitlePropertyName);
                _applicationTitle = value;
                RaisePropertyChanged(ApplicationTitlePropertyName);
            }
        }
        #endregion

        #region WelcomeTitle
        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "DumbSearch by Patware";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }
        #endregion

        #region Root

        /// <summary>
        /// The <see cref="Root" /> property's name.
        /// </summary>
        public const string RootPropertyName = "Root";

        private string _root = string.Empty;

        /// <summary>
        /// Sets and gets the Root property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Root
        {
            get
            {
                return _root;
            }

            set
            {
                if (_root == value)
                {
                    return;
                }

                RaisePropertyChanging(RootPropertyName);
                _root = value;
                RaisePropertyChanged(RootPropertyName);
            }
        }

        #endregion

        #region Folder

        /// <summary>
        /// The <see cref="Folder" /> property's name.
        /// </summary>
        public const string FolderPropertyName = "Folder";

        private string _folder = string.Empty;

        /// <summary>
        /// Sets and gets the Folder property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Folder
        {
            get
            {
                return _folder;
            }

            set
            {
                if (_folder == value)
                {
                    return;
                }

                RaisePropertyChanging(FolderPropertyName);
                _folder = value;
                RaisePropertyChanged(FolderPropertyName);
            }
        }

        #endregion

        #region FolderIsRegex

        /// <summary>
        /// The <see cref="FolderIsRegex" /> property's name.
        /// </summary>
        public const string FolderIsRegexPropertyName = "FolderIsRegex";

        private bool _folderIsRegex = false;

        /// <summary>
        /// Sets and gets the FolderIsRegex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool FolderIsRegex
        {
            get
            {
                return _folderIsRegex;
            }

            set
            {
                if (_folderIsRegex == value)
                {
                    return;
                }

                RaisePropertyChanging(FolderIsRegexPropertyName);
                _folderIsRegex = value;
                RaisePropertyChanged(FolderIsRegexPropertyName);
            }
        }

        #endregion

        #region File

        /// <summary>
        /// The <see cref="File" /> property's name.
        /// </summary>
        public const string FilePropertyName = "File";

        private string _file = string.Empty;

        /// <summary>
        /// Sets and gets the File property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string File
        {
            get
            {
                return _file;
            }

            set
            {
                if (_file == value)
                {
                    return;
                }

                RaisePropertyChanging(FilePropertyName);
                _file = value;
                RaisePropertyChanged(FilePropertyName);
            }
        }

        #endregion

        #region FileIsRegex

        /// <summary>
        /// The <see cref="FileIsRegex" /> property's name.
        /// </summary>
        public const string FileIsRegexPropertyName = "FileIsRegex";

        private bool _fileIsRegex = false;

        /// <summary>
        /// Sets and gets the FileIsRegex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool FileIsRegex
        {
            get
            {
                return _fileIsRegex;
            }

            set
            {
                if (_fileIsRegex == value)
                {
                    return;
                }

                RaisePropertyChanging(FileIsRegexPropertyName);
                _fileIsRegex = value;
                RaisePropertyChanged(FileIsRegexPropertyName);
            }
        }

        #endregion

        #region Content

        /// <summary>
        /// The <see cref="Content" /> property's name.
        /// </summary>
        public const string ContentPropertyName = "Content";

        private string _content = string.Empty;

        /// <summary>
        /// Sets and gets the Content property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (_content == value)
                {
                    return;
                }

                RaisePropertyChanging(ContentPropertyName);
                _content = value;
                RaisePropertyChanged(ContentPropertyName);
            }
        }

        #endregion

        #region ContentIsRegex

        /// <summary>
        /// The <see cref="ContentIsRegex" /> property's name.
        /// </summary>
        public const string ContentIsRegexPropertyName = "ContentIsRegex";

        private bool _contentIsRegex = false;

        /// <summary>
        /// Sets and gets the ContentIsRegex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ContentIsRegex
        {
            get
            {
                return _contentIsRegex;
            }

            set
            {
                if (_contentIsRegex == value)
                {
                    return;
                }

                RaisePropertyChanging(ContentIsRegexPropertyName);
                _contentIsRegex = value;
                RaisePropertyChanged(ContentIsRegexPropertyName);
            }
        }

        #endregion

        #region DateCreatedIsChecked

        /// <summary>
        /// The <see cref="DateCreatedIsChecked" /> property's name.
        /// </summary>
        public const string DateCreatedIsCheckedPropertyName = "DateCreatedIsChecked";

        private bool _dateCreatedIsChecked = false;

        /// <summary>
        /// Sets and gets the DateCreatedIsChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DateCreatedIsChecked
        {
            get
            {
                return _dateCreatedIsChecked;
            }

            set
            {
                if (_dateCreatedIsChecked == value)
                {
                    return;
                }

                RaisePropertyChanging(DateCreatedIsCheckedPropertyName);
                _dateCreatedIsChecked = value;
                RaisePropertyChanged(DateCreatedIsCheckedPropertyName);
            }
        }

        #endregion

        #region DateCreatedOperator

        /// <summary>
        /// The <see cref="DateCreatedOperator" /> property's name.
        /// </summary>
        public const string DateCreatedOperatorPropertyName = "DateCreatedOperator";

        private string _dateCreatedOperator = string.Empty;

        /// <summary>
        /// Sets and gets the DateCreatedOperator property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DateCreatedOperator
        {
            get
            {
                return _dateCreatedOperator;
            }

            set
            {
                if (_dateCreatedOperator == value)
                {
                    return;
                }

                RaisePropertyChanging(DateCreatedOperatorPropertyName);
                _dateCreatedOperator = value;
                RaisePropertyChanged(DateCreatedOperatorPropertyName);
            }
        }

        #endregion

        #region DateCreatedFrom

        /// <summary>
        /// The <see cref="DateCreatedFrom" /> property's name.
        /// </summary>
        public const string DateCreatedFromPropertyName = "DateCreatedFrom";

        private DateTime _dateCreatedFrom = DateTime.Now;

        /// <summary>
        /// Sets and gets the DateCreatedFrom property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime DateCreatedFrom
        {
            get
            {
                return _dateCreatedFrom;
            }

            set
            {
                if (_dateCreatedFrom == value)
                {
                    return;
                }

                RaisePropertyChanging(DateCreatedFromPropertyName);
                _dateCreatedFrom = value;
                RaisePropertyChanged(DateCreatedFromPropertyName);
            }
        }

        #endregion

        #region DateCreatedTo

        /// <summary>
        /// The <see cref="DateCreatedTo" /> property's name.
        /// </summary>
        public const string DateCreatedToPropertyName = "DateCreatedTo";

        private DateTime _dateCreatedTo = DateTime.Now;

        /// <summary>
        /// Sets and gets the DateCreatedTo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime DateCreatedTo
        {
            get
            {
                return _dateCreatedTo;
            }

            set
            {
                if (_dateCreatedTo == value)
                {
                    return;
                }

                RaisePropertyChanging(DateCreatedToPropertyName);
                _dateCreatedTo = value;
                RaisePropertyChanged(DateCreatedToPropertyName);
            }
        }

        #endregion

        #region DateCreatedToIsVisible

        /// <summary>
        /// The <see cref="DateCreatedToIsVisible" /> property's name.
        /// </summary>
        public const string DateCreatedToIsVisiblePropertyName = "DateCreatedToIsVisible";

        private bool _dateCreatedToIsVisible = false;

        /// <summary>
        /// Sets and gets the DateCreatedToIsVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DateCreatedToIsVisible
        {
            get
            {
                return _dateCreatedToIsVisible;
            }

            set
            {
                if (_dateCreatedToIsVisible == value)
                {
                    return;
                }

                RaisePropertyChanging(DateCreatedToIsVisiblePropertyName);
                _dateCreatedToIsVisible = value;
                RaisePropertyChanged(DateCreatedToIsVisiblePropertyName);
            }
        }

        #endregion

        #region DateModifiedIsChecked

        /// <summary>
        /// The <see cref="DateModifiedIsChecked" /> property's name.
        /// </summary>
        public const string DateModifiedIsCheckedPropertyName = "DateModifiedIsChecked";

        private bool _dateModifiedIsChecked = false;

        /// <summary>
        /// Sets and gets the DateModifiedIsChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DateModifiedIsChecked
        {
            get
            {
                return _dateModifiedIsChecked;
            }

            set
            {
                if (_dateModifiedIsChecked == value)
                {
                    return;
                }

                RaisePropertyChanging(DateModifiedIsCheckedPropertyName);
                _dateModifiedIsChecked = value;
                RaisePropertyChanged(DateModifiedIsCheckedPropertyName);
            }
        }

        #endregion

        #region DateModifiedOperator

        /// <summary>
        /// The <see cref="DateModifiedOperator" /> property's name.
        /// </summary>
        public const string DateModifiedOperatorPropertyName = "DateModifiedOperator";

        private string _dateModifiedOperator = string.Empty;

        /// <summary>
        /// Sets and gets the DateModifiedOperator property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DateModifiedOperator
        {
            get
            {
                return _dateModifiedOperator;
            }

            set
            {
                if (_dateModifiedOperator == value)
                {
                    return;
                }

                RaisePropertyChanging(DateModifiedOperatorPropertyName);
                _dateModifiedOperator = value;
                RaisePropertyChanged(DateModifiedOperatorPropertyName);
            }
        }

        #endregion

        #region DateModifiedFrom

        /// <summary>
        /// The <see cref="DateModifiedFrom" /> property's name.
        /// </summary>
        public const string DateModifiedFromPropertyName = "DateModifiedFrom";

        private DateTime _dateModifiedFrom = DateTime.Now;

        /// <summary>
        /// Sets and gets the DateModifiedFrom property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime DateModifiedFrom
        {
            get
            {
                return _dateModifiedFrom;
            }

            set
            {
                if (_dateModifiedFrom == value)
                {
                    return;
                }

                RaisePropertyChanging(DateModifiedFromPropertyName);
                _dateModifiedFrom = value;
                RaisePropertyChanged(DateModifiedFromPropertyName);
            }
        }

        #endregion

        #region DateModifiedTo

        /// <summary>
        /// The <see cref="DateModifiedTo" /> property's name.
        /// </summary>
        public const string DateModifiedToPropertyName = "DateModifiedTo";

        private DateTime _dateModifiedTo = DateTime.Now;

        /// <summary>
        /// Sets and gets the DateModifiedTo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime DateModifiedTo
        {
            get
            {
                return _dateModifiedTo;
            }

            set
            {
                if (_dateModifiedTo == value)
                {
                    return;
                }

                RaisePropertyChanging(DateModifiedToPropertyName);
                _dateModifiedTo = value;
                RaisePropertyChanged(DateModifiedToPropertyName);
            }
        }

        #endregion

        #region DateModifiedToIsVisible

        /// <summary>
        /// The <see cref="DateModifiedToIsVisible" /> property's name.
        /// </summary>
        public const string DateModifiedToIsVisiblePropertyName = "DateModifiedToIsVisible";

        private bool _dateModifiedToIsVisible = false;

        /// <summary>
        /// Sets and gets the DateModifiedToIsVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DateModifiedToIsVisible
        {
            get
            {
                return _dateModifiedToIsVisible;
            }

            set
            {
                if (_dateModifiedToIsVisible == value)
                {
                    return;
                }

                RaisePropertyChanging(DateModifiedToIsVisiblePropertyName);
                _dateModifiedToIsVisible = value;
                RaisePropertyChanged(DateModifiedToIsVisiblePropertyName);
            }
        }

        #endregion

        #region SizeIsChecked

        /// <summary>
        /// The <see cref="SizeIsChecked" /> property's name.
        /// </summary>
        public const string SizeIsCheckedPropertyName = "SizeIsChecked";

        private bool _sizeIsChecked = false;

        /// <summary>
        /// Sets and gets the SizeIsChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SizeIsChecked
        {
            get
            {
                return _sizeIsChecked;
            }

            set
            {
                if (_sizeIsChecked == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeIsCheckedPropertyName);
                _sizeIsChecked = value;
                RaisePropertyChanged(SizeIsCheckedPropertyName);
            }
        }

        #endregion

        #region SizeOperator

        /// <summary>
        /// The <see cref="SizeOperator" /> property's name.
        /// </summary>
        public const string SizeOperatorPropertyName = "SizeOperator";

        private string _sizeOperator = string.Empty;

        /// <summary>
        /// Sets and gets the SizeOperator property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SizeOperator
        {
            get
            {
                return _sizeOperator;
            }

            set
            {
                if (_sizeOperator == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeOperatorPropertyName);
                _sizeOperator = value;
                RaisePropertyChanged(SizeOperatorPropertyName);
            }
        }

        #endregion

        #region SizeFrom

        /// <summary>
        /// The <see cref="SizeFrom" /> property's name.
        /// </summary>
        public const string SizeFromPropertyName = "SizeFrom";

        private int _sizeFrom = 0;

        /// <summary>
        /// Sets and gets the SizeFrom property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SizeFrom
        {
            get
            {
                return _sizeFrom;
            }

            set
            {
                if (_sizeFrom == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeFromPropertyName);
                _sizeFrom = value;
                RaisePropertyChanged(SizeFromPropertyName);
            }
        }

        #endregion

        #region SizeFromUnit

        /// <summary>
        /// The <see cref="SizeFromUnit" /> property's name.
        /// </summary>
        public const string SizeFromUnitPropertyName = "SizeFromUnit";

        private string _sizeFromUnit = string.Empty;

        /// <summary>
        /// Sets and gets the SizeFromUnit property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SizeFromUnit
        {
            get
            {
                return _sizeFromUnit;
            }

            set
            {
                if (_sizeFromUnit == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeFromUnitPropertyName);
                _sizeFromUnit = value;
                RaisePropertyChanged(SizeFromUnitPropertyName);
            }
        }

        #endregion

        #region SizeTo

        /// <summary>
        /// The <see cref="SizeTo" /> property's name.
        /// </summary>
        public const string SizeToPropertyName = "SizeTo";

        private int _sizeTo = 0;

        /// <summary>
        /// Sets and gets the SizeTo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SizeTo
        {
            get
            {
                return _sizeTo;
            }

            set
            {
                if (_sizeTo == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeToPropertyName);
                _sizeTo = value;
                RaisePropertyChanged(SizeToPropertyName);
            }
        }

        #endregion

        #region SizeToIsVisible

        /// <summary>
        /// The <see cref="SizeToIsVisible" /> property's name.
        /// </summary>
        public const string SizeToIsVisiblePropertyName = "SizeToIsVisible";

        private bool _sizeToIsVisible = false;

        /// <summary>
        /// Sets and gets the SizeToIsVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SizeToIsVisible
        {
            get
            {
                return _sizeToIsVisible;
            }

            set
            {
                if (_sizeToIsVisible == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeToIsVisiblePropertyName);
                _sizeToIsVisible = value;
                RaisePropertyChanged(SizeToIsVisiblePropertyName);
            }
        }

        #endregion

        #region SizeToUnit

        /// <summary>
        /// The <see cref="SizeToUnit" /> property's name.
        /// </summary>
        public const string SizeToUnitPropertyName = "SizeToUnit";

        private string _sizeToUnit = string.Empty;

        /// <summary>
        /// Sets and gets the SizeToUnit property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SizeToUnit
        {
            get
            {
                return _sizeToUnit;
            }

            set
            {
                if (_sizeToUnit == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeToUnitPropertyName);
                _sizeToUnit = value;
                RaisePropertyChanged(SizeToUnitPropertyName);
            }
        }

        #endregion

        #region SizeToUnitIsVisible

        /// <summary>
        /// The <see cref="SizeToUnitIsVisible" /> property's name.
        /// </summary>
        public const string SizeToUnitIsVisiblePropertyName = "SizeToUnitIsVisible";

        private bool _sizeToUnitIsVisible = false;

        /// <summary>
        /// Sets and gets the SizeToUnitIsVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SizeToUnitIsVisible
        {
            get
            {
                return _sizeToUnitIsVisible;
            }

            set
            {
                if (_sizeToUnitIsVisible == value)
                {
                    return;
                }

                RaisePropertyChanging(SizeToUnitIsVisiblePropertyName);
                _sizeToUnitIsVisible = value;
                RaisePropertyChanged(SizeToUnitIsVisiblePropertyName);
            }
        }

        #endregion

        #region FoldersFound

        /// <summary>
        /// The <see cref="FoldersFound" /> property's name.
        /// </summary>
        public const string FoldersFoundPropertyName = "FoldersFound";

        private string _foldersFound = string.Empty;

        /// <summary>
        /// Sets and gets the FoldersFound property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FoldersFound
        {
            get
            {
                return _foldersFound;
            }

            set
            {
                if (_foldersFound == value)
                {
                    return;
                }

                RaisePropertyChanging(FoldersFoundPropertyName);
                _foldersFound = value;
                RaisePropertyChanged(FoldersFoundPropertyName);
            }
        }

        #endregion

        #region FoldersSearched

        /// <summary>
        /// The <see cref="FoldersSearched" /> property's name.
        /// </summary>
        public const string FoldersSearchedPropertyName = "FoldersSearched";

        private string _foldersSearched = string.Empty;

        /// <summary>
        /// Sets and gets the FoldersSearched property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FoldersSearched
        {
            get
            {
                return _foldersSearched;
            }

            set
            {
                if (_foldersSearched == value)
                {
                    return;
                }

                RaisePropertyChanging(FoldersSearchedPropertyName);
                _foldersSearched = value;
                RaisePropertyChanged(FoldersSearchedPropertyName);
            }
        }

        #endregion

        #region FoldersMatched

        /// <summary>
        /// The <see cref="FoldersMatched" /> property's name.
        /// </summary>
        public const string FoldersMatchedPropertyName = "FoldersMatched";

        private string _foldersMatched = string.Empty;

        /// <summary>
        /// Sets and gets the FoldersMatched property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FoldersMatched
        {
            get
            {
                return _foldersMatched;
            }

            set
            {
                if (_foldersMatched == value)
                {
                    return;
                }

                RaisePropertyChanging(FoldersMatchedPropertyName);
                _foldersMatched = value;
                RaisePropertyChanged(FoldersMatchedPropertyName);
            }
        }

        #endregion

        #region FilesFound

        /// <summary>
        /// The <see cref="FilesFound" /> property's name.
        /// </summary>
        public const string FilesFoundPropertyName = "FilesFound";

        private string _filesFound = string.Empty;

        /// <summary>
        /// Sets and gets the FilesFound property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FilesFound
        {
            get
            {
                return _filesFound;
            }

            set
            {
                if (_filesFound == value)
                {
                    return;
                }

                RaisePropertyChanging(FilesFoundPropertyName);
                _filesFound = value;
                RaisePropertyChanged(FilesFoundPropertyName);
            }
        }

        #endregion

        #region FilesSearched

        /// <summary>
        /// The <see cref="FilesSearched" /> property's name.
        /// </summary>
        public const string FilesSearchedPropertyName = "FilesSearched";

        private string _filesSearched = string.Empty;

        /// <summary>
        /// Sets and gets the FilesSearched property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FilesSearched
        {
            get
            {
                return _filesSearched;
            }

            set
            {
                if (_filesSearched == value)
                {
                    return;
                }

                RaisePropertyChanging(FilesSearchedPropertyName);
                _filesSearched = value;
                RaisePropertyChanged(FilesSearchedPropertyName);
            }
        }

        #endregion

        #region FilesMatched

        /// <summary>
        /// The <see cref="FilesMatched" /> property's name.
        /// </summary>
        public const string FilesMatchedPropertyName = "FilesMatched";

        private string _filesMatched = string.Empty;

        /// <summary>
        /// Sets and gets the FilesMatched property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FilesMatched
        {
            get
            {
                return _filesMatched;
            }

            set
            {
                if (_filesMatched == value)
                {
                    return;
                }

                RaisePropertyChanging(FilesMatchedPropertyName);
                _filesMatched = value;
                RaisePropertyChanged(FilesMatchedPropertyName);
            }
        }

        #endregion

        #region ContentMatchingProgress

        /// <summary>
        /// The <see cref="ContentMatchingProgress" /> property's name.
        /// </summary>
        public const string ContentMatchingProgressPropertyName = "ContentMatchingProgress";

        private int _contentMatchingProgress = 0;

        /// <summary>
        /// Sets and gets the ContentMatchingProgress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ContentMatchingProgress
        {
            get
            {
                return _contentMatchingProgress;
            }

            set
            {
                if (_contentMatchingProgress == value)
                {
                    return;
                }

                RaisePropertyChanging(ContentMatchingProgressPropertyName);
                _contentMatchingProgress = value;
                RaisePropertyChanged(ContentMatchingProgressPropertyName);
            }
        }

        #endregion

        #region ContentMatchingProgressIsVisible

        /// <summary>
        /// The <see cref="ContentMatchingProgressIsVisible" /> property's name.
        /// </summary>
        public const string ContentMatchingProgressIsVisiblePropertyName = "ContentMatchingProgressIsVisible";

        private bool _contentMatchingProgressIsVisible = false;

        /// <summary>
        /// Sets and gets the ContentMatchingProgressIsVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ContentMatchingProgressIsVisible
        {
            get
            {
                return _contentMatchingProgressIsVisible;
            }

            set
            {
                if (_contentMatchingProgressIsVisible == value)
                {
                    return;
                }

                RaisePropertyChanging(ContentMatchingProgressIsVisiblePropertyName);
                _contentMatchingProgressIsVisible = value;
                RaisePropertyChanged(ContentMatchingProgressIsVisiblePropertyName);
            }
        }

        #endregion

        #region FoundItems

        /// <summary>
        /// The <see cref="FoundItems" /> property's name.
        /// </summary>
        public const string FoundItemsPropertyName = "FoundItems";

        private ObservableCollection<string> _foundItems = new ObservableCollection<string>();

        /// <summary>
        /// Sets and gets the FoundItems property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> FoundItems
        {
            get
            {
                return _foundItems;
            }

        }

        #endregion

        #region CurrentContentMatchingMessage

        /// <summary>
        /// The <see cref="CurrentContentMatchingMessage" /> property's name.
        /// </summary>
        public const string CurrentContentMatchingMessagePropertyName = "CurrentContentMatchingMessage";

        private string _currentContentMatchingMessage = string.Empty;

        /// <summary>
        /// Sets and gets the CurrentContentMatchingMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentContentMatchingMessage
        {
            get
            {
                return _currentContentMatchingMessage;
            }

            set
            {
                if (_currentContentMatchingMessage == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentContentMatchingMessagePropertyName);
                _currentContentMatchingMessage = value;
                RaisePropertyChanged(CurrentContentMatchingMessagePropertyName);
            }
        }

        #endregion

        #endregion

        #region Commands
        private RelayCommand _search;

        /// <summary>
        /// Gets the Search.
        /// </summary>
        public RelayCommand Search
        {
            get
            {
                return _search
                    ?? (_search = new RelayCommand(
                                          () =>
                                          {
                                              if (string.IsNullOrEmpty(_root))
                                              {
                                                  this.Root = System.IO.Directory.GetCurrentDirectory();
                                              }

                                              var searchParameters = new Model.SearchParameters()
                                              {
                                                  Root = _root,
                                                  Folder = _folder,
                                                  FolderIsRegex = _folderIsRegex,
                                                  FileName = _file,
                                                  FileNameIsRegex = _fileIsRegex,
                                                  Content = _content,
                                                  ContentIsRegex = _contentIsRegex
                                              };

                                              var searchHelper = new Services.SearchHelper();
                                              //Model.Results results;

                                              var t = System.Threading.Tasks.Task.Factory.StartNew(() => {
                                                  searchHelper.Search(searchParameters);

                                                  this.CurrentContentMatchingMessage = string.Empty;
                                                  this.ContentMatchingProgress = 0;
                                                  this.ContentMatchingProgressIsVisible = false;

                                              });

                                          },
                                          () => true));
            }
        }
        #endregion

        #region Messaging

        private void doSearchStarted(Messages.SearchStarted searchStarted)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                (Action)delegate()
            {
                this.FoundItems.Clear();
            });
        }
        public void doThereIsProgress(Messages.ThereIsProgress thereIsProgress)
        {
            this.FoldersFound = thereIsProgress.Content.FoldersFound.ToString();
            this.FoldersSearched = thereIsProgress.Content.FoldersSearched.ToString();
            this.FoldersMatched = thereIsProgress.Content.FoldersMatched.ToString();

            this.FilesFound = thereIsProgress.Content.FilesFound.ToString();
            this.FilesSearched = thereIsProgress.Content.FilesSearched.ToString();
            this.FilesMatched = thereIsProgress.Content.FilesMatched.ToString();

            this.ContentMatchingProgressIsVisible = true;
            this.ContentMatchingProgress = thereIsProgress.Content.ContentMatchingProgress;
            this.CurrentContentMatchingMessage = thereIsProgress.Content.CurrentFile;


        }

        private System.Collections.Generic.IList<FileInfo> _matchedFileList = new System.Collections.Generic.List<FileInfo>();

        public void doFileMatched(Messages.FileMatched fileMatched)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                   System.Windows.Threading.DispatcherPriority.Normal,
                   (Action)delegate()
                   {
                       this.FoundItems.Add(fileMatched.Content.FullName);
                   });
        }

        private void doContentMatched(Messages.ContentMatched contentMatched)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                (Action)delegate()
                {
                    this.FoundItems.Add(
                        string.Format(
                            "{0} on line {1}"
                            , contentMatched.Content.FullName
                            , contentMatched.LineNumber));
                });
        }

        #endregion
    }
}