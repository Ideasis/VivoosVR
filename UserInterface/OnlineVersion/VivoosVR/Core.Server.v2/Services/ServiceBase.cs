using Core.Log;
using Core.Server.Brokers.Organization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Services
{
    public class ServiceBase<T> where T : DbContext
    {
        public T Entities { get; set; }
        public UserBroker UserBroker { get; set; }

        public ServiceBase(T entities)
        {
            Entities = entities;
            Entities.Configuration.LazyLoadingEnabled = false;
            Entities.Configuration.ProxyCreationEnabled = false;

            var v = GetCurrentUser();
            v.LogInformation();

            UserBroker = UserBroker.Init().Load(GetCurrentUser());

            //GroupBroker = GroupBroker.Load(UserBroker.User.GroupId);
            //BranchBroker = BranchBroker.Init().Load(GroupBroker.Group.BranchId);
            //CompanyBroker = CompanyBroker.Init().Load(BranchBroker.Branch.CompanyId);
        }

        public string GetCurrentUser()
        {
            return ServiceSecurityContext.Current.PrimaryIdentity.Name;
        }
    }
}
