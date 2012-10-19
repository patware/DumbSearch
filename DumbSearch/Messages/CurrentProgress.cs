using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Messages
{
    public class CurrentProgress
    {
        public int FoldersFound { get; set; }
        public int FoldersSearched { get; set; }
        public int FoldersMatched { get; set; }
        public int FilesFound { get; set; }
        public int FilesSearched { get; set; }
        public int FilesMatched { get; set; }
        public string CurrentFile { get; set; }
        public int ContentMatchingProgress { get; set; }
    }
}
