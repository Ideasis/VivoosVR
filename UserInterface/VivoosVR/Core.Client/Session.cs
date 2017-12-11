using Core.Client.Configuration;
using Core.Common.Configuration;
using Core.Common.DataModel.Core;
using Core.Common.Services;
using Core.Log;
using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client
{
    public class Session : ISession
    {
        public User CurrentUser { get; set; }

        public Session()
        {
            ISessionService sessionService = GetProxy<ISessionService>();
            CurrentUser = sessionService.GetDefaultUser();
        }

        public bool AmIAllowed(string resource)
        {
            return CurrentUser.RoleToUserMaps.Select(x => x.Role).SelectMany(x => x.Claims).Any(x => x.Resource == resource);
        }

        public bool DoIHaveRole(string code)
        {
            return CurrentUser.RoleToUserMaps.Select(x => x.Role).Any(x => x.Code == code);
        }

        public T GetProxy<T>(string configurationName = "*")
        {
            WSHttpBinding binding = new WSHttpBinding();
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(configurationName);

            if (CurrentUser != null)
            {
                channelFactory.Credentials.UserName.UserName = CurrentUser.Consumer.Code;
                channelFactory.Credentials.UserName.Password = CurrentUser.Password;
            }
            else
            {
                channelFactory.Credentials.UserName.UserName = CommonConfiguration.Configuration.DefaultUser.Username;
                channelFactory.Credentials.UserName.Password = CommonConfiguration.Configuration.DefaultUser.Password;
            }

            T ret = channelFactory.CreateChannel();

            //if (ret == null)
            //{
            //    throw new UserException("Sunucu bağlantısı kurulurken hata oluştu.");
            //}

            return ret;
        }

        public Session SignIn(string username, string password)
        {
            ISessionService sessionService = GetProxy<ISessionService>();

            CurrentUser = sessionService.SignIn(username, password);

            // Save last succesful username
            Core.Client.Properties.Settings.Default.LastUsername = CurrentUser.Consumer.Code;
            Core.Client.Properties.Settings.Default.Save();


            return this;
        }

        public void SignOut()
        {
            CurrentUser = null;
        }

        public void SubscribeProcess(ProcessHub process)
        {

        }
    }
}
