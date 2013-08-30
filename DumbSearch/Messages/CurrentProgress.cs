using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Messages
{
    public class CurrentProgress
    {
        public int FoldersDiscovered { get; set; }
        public int FoldersMatched { get; set; }
        public int FoldersSurveyed { get; set; }

        public int FilesDiscovered { get; set; }
        public int FilesMatched { get; set; }
        public int FilesSurveyed { get; set; }
        
        public string CurrentFolder { get; set; }
        public string CurrentFile { get; set; }
        public int ContentMatchingProgress { get; set; }
    }
}
