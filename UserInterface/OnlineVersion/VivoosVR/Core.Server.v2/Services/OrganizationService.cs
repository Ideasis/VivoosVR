using Core.Common;
using Core.Common.Services;
using Core.Server;
using Core.Server.Brokers;
using Core.Server.Brokers.Organization;
using Core.Server.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Core.Server.Services
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ErrorBehavior(typeof(CoreErrorHandler))]
    public class OrganizationService : IOrganizationService
    {
        public string Ping()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}
