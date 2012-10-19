using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DumbSearch.Messages
{
    public class ContentMatched:GalaSoft.MvvmLight.Messaging.GenericMessage<FileInfo>
    {
        public int LineNumber { get; set; }
        
        public ContentMatched(FileInfo file) : base(file)
        {
            
        }
    }
}
