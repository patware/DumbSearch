using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DumbSearch.Messages
{
    public class FileMatched:GalaSoft.MvvmLight.Messaging.GenericMessage<FileInfo>
    {
        public FileMatched(FileInfo foundFile):base(foundFile)
        {

        }
    }
}
