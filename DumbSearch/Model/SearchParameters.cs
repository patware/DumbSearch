using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Model
{
    public class SearchParameters
    {
        public string Root { get; set; }
        public string Folder { get; set; }
        public bool FolderIsRegex { get; set; }
        public string FileName { get; set; }
        public bool FileNameIsRegex { get; set; }
        public string Content { get; set; }
        public bool ContentIsRegex { get; set; }

    }
}
