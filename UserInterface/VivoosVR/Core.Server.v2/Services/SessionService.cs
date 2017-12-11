using Core.Common.DataModel.Core;
using Core.Common.Services;
using Core.Log.Exceptions;
using Core.Log.Faults;
using Core.Server.Brokers;
using Core.Server.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Services
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ErrorBehavior(typeof(CoreErrorHandler))]
    public class SessionService : ISessionService
    {
        public SessionService()
        {
        }

        public User GetDefaultUser()
        {
            var ret = SessionBroker
                .Init()
                .LoadDefaultUser()
                .CurrentUser;

            return ret;
        }

        public User SignIn(string code, string password)
        {
            var ret = SessionBroker
                .Init()
                .SignIn(code, password)
                .CurrentUser;

            return ret;
        }
    }
}
