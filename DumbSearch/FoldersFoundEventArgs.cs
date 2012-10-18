using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DumbSearch
{
    public class FoldersFoundEventArgs : EventArgs
    {
        #region Private Variables

        private List<DirectoryInfo> _folders;

        #endregion



        #region Public Properties
        ///<summary> TODO: Documentation</summary>
        public List<DirectoryInfo> Folders
        {
            get { return _folders; }
            set { _folders = value; }
        }

        #endregion


        public FoldersFoundEventArgs()
        {
            _folders = new List<DirectoryInfo>();
        }
        public FoldersFoundEventArgs(List<DirectoryInfo> folders)
        {
            _folders = folders;
        }
    }
}
