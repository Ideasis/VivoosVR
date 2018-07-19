using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class PasswordChangedMessage : IMessageBase
    {
        public Observable<string> Password { get; set; }

        public PasswordChangedMessage(string password)
        {
            Password = new Observable<string>(password);
        }
    }
}
