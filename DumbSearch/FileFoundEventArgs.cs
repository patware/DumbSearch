using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DumbSearch
{
    public class FileFoundEventArgs : EventArgs
    {
        #region Private Variables

        private FileInfo _file;

        #endregion
        
        #region Public Properties
        ///<summary> TODO: Documentation</summary>
        public FileInfo File
        {
            get { return _file; }
            set { _file = value; }
        }

        #endregion

        public FileFoundEventArgs()
        {

        }

        public FileFoundEventArgs(FileInfo file)
        {
            _file = file;
        }


    }
}
