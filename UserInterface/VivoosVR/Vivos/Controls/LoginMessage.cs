using Core.Common.DataModel.Core;
using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class LoginMessage : IMessageBase
    {
        public Observable<User> UserData { get; set; }

        public LoginMessage(User userData)
        {
            UserData = new Observable<User>(userData);
        }
    }
}
