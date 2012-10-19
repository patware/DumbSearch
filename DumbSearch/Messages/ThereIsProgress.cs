using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Messages
{
    public class ThereIsProgress: GalaSoft.MvvmLight.Messaging.GenericMessage<CurrentProgress>
    {
        public ThereIsProgress(CurrentProgress progress)
            : base(progress)
        {

        }
    }
}
