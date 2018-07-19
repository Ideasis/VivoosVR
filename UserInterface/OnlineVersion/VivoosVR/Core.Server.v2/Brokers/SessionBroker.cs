using Core.Common;
using Core.Common.Configuration;
using Core.Common.Constants;
using Core.Common.DataModel.Core;
using Core.Log.Exceptions;
using Core.Server.Brokers.Organization;
using Core.Server.Constants;
using Core.Server.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers
{
    public class SessionBroker : CoreBaseBroker
    {
        public User CurrentUser { get; set; }
        public UserBroker OrganizationBroker { get; set; }

        private SessionBroker()
        {
            NewOrganizationBroker();
            LoadDefaultUser();
        }

        public bool AmIAllowed(string resource)
        {
            return CurrentUser.RoleToUserMaps
                .Where(x => x.ValidUntil > DateTime.Now || x.ValidUntil == null)
                .SelectMany(x => x.Role.Claims)
                .Any(x => x.Available && x.Resource == resource);

        }

        public AuthenticationStatus CheckUsernameAndPassword(string code, string password)
        {
            bool? isExpired = false, isPasswordCorrect = false;

            OrganizationBroker
                .Load(code, true)
                .IsExpired(out isExpired)
                .IsPasswordEqual(password, out isPasswordCorrect);

            if (OrganizationBroker.User == null || isPasswordCorrect == false || string.IsNullOrWhiteSpace(code))
            {
                return AuthenticationStatus.UnknownUser;
            }

            if (OrganizationBroker.User.Consumer.Code == CommonConfiguration.Configuration.DefaultUser.Username)
            {
                return AuthenticationStatus.DefaultUser;
            }

            if (isExpired == true)
            {
                return AuthenticationStatus.Expired;
            }

            return AuthenticationStatus.Succeed;
        }

        public static SessionBroker Init()
        {
            SessionBroker ret = new SessionBroker();

            return ret;
        }

        public bool IsSignedIn()
        {
            return CurrentUser != null;
        }

        public SessionBroker LoadDefaultUser()
        {
            OrganizationBroker.Load(CommonConfiguration.Configuration.DefaultUser.Username, true);
            CurrentUser = OrganizationBroker.User;

            return this;
        }

        public SessionBroker NewOrganizationBroker()
        {
            OrganizationBroker = UserBroker.Init();

            return this;
        }

        public SessionBroker SignIn(string code, string password)
        {
            switch (CheckUsernameAndPassword(code, password))
            {
                case AuthenticationStatus.Succeed:
                    CurrentUser = OrganizationBroker.User;
                    break;
                case AuthenticationStatus.DefaultUser:
                    throw new ApplicationException("Varsayılan kullanıcı sisteme giriş yapamaz.");
                case AuthenticationStatus.UnknownUser:
                    throw new ApplicationException("Geçersiz Kullanıcı Adı/Şifre"); //new FaultException<AuthenticationException>(new AuthenticationException(code, "Geçersiz Kullanıcı Adı/Şifre"), new FaultReason("Geçersiz Kullanıcı Adı/Şifre"));
                case AuthenticationStatus.Expired:
                    throw new ApplicationException("Belirtilen kullanıcının kullanım tarihi dolmuştur.");
                default:
                    CurrentUser = null;
                    break;
            }

            return this;
        }

        public SessionBroker SignOut()
        {
            LoadDefaultUser();

            return this;
        }
    }
}
