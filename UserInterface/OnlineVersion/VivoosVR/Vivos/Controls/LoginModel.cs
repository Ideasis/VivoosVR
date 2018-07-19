using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class LoginModel
    {
        public string Error
        {
            get { return null; }
        }
        public Observable<string> Password { get; set; }
        public Observable<string> Username { get; set; }

        public LoginModel()
        {
            Username = new Observable<string>(Environment.UserName);
            Password = new Observable<string>();

            Username.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(Username.Value))
                {
                    return "Kullanıcı Adı alanı mutlaka dolu olmalıdır.";
                }
                return null;
            });

            Password.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(Password.Value.ToString()))
                {
                    return "Şifre alanı mutlaka dolu olmalıdır.";
                }
                return null;
            });
        }
    }
}
