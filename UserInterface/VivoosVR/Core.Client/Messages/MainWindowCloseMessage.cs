using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client.Messages
{
    public class MainWindowCloseMessage : IMessageBase
    {
        public bool IsCancel { get; set; }
    }
}
