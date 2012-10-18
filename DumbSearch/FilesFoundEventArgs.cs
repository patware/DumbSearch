using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DumbSearch
{
    public class FilesFoundEventArgs : EventArgs
    {
        #region Private Variables

        private List<FileInfo> _files;

        #endregion



        #region Public Properties
        ///<summary> TODO: Documentation</summary>
        public List<FileInfo> Files
        {
            get { return _files; }
            set { _files = value; }
        }

        #endregion

        public FilesFoundEventArgs()
        {
            _files = new List<FileInfo>();
        }
        public FilesFoundEventArgs(List<FileInfo> files)
        {
            _files = files;
        }
    }
}
