using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Messages
{
    public class SearchStarted : GalaSoft.MvvmLight.Messaging.GenericMessage<string>
    {
        public SearchStarted(string message)
            : base(message)
        {
        }
    }
}
