using Core.Common;
using Core.Common.Configuration;
using Core.Common.DataModel.Core;
using Core.Log;
using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers.Organization
{
    public class UserBroker : ConsumerBroker
    {
        public User User { get; set; }
        public List<User> Users { get; set; }

        private UserBroker()
        {
        }

        public UserBroker DeleteUser()
        {
            CoreEntities.Users.Remove(User);
            return this;
        }

        public IEnumerable<Claim> GetClaims()
        {
            IEnumerable<Claim> ret = null;

            if (User == null)
            {
                "User is not set, claims cannot be obtained.".LogCritical();
                return null;
            }

            ret = User.RoleToUserMaps
                .Where(x => x.ValidUntil > DateTime.Now || x.ValidUntil == null && x.Available)
                .SelectMany(x => x.Role.Claims)
                .Where(x => x.Available);

            return ret;
        }

        public IEnumerable<RoleToUserMap> GetRoleMaps()
        {
            IEnumerable<RoleToUserMap> ret = null;

            ret = User.RoleToUserMaps
                .Where(x => x.ValidUntil > DateTime.Now || x.ValidUntil == null && x.Available);

            return ret;
        }

        public IEnumerable<Role> GetRoles()
        {
            IEnumerable<Role> ret = null;

            ret = User.RoleToUserMaps
                .Where(x => x.ValidUntil > DateTime.Now || x.ValidUntil == null && x.Available)
                .Select(x => x.Role);

            return ret;
        }

        public static UserBroker Init()
        {
            UserBroker ret = new UserBroker();

            return ret;
        }

        public UserBroker IsExpired(out bool? isExpired)
        {
            if (User == null)
            {
                isExpired = null;
            }
            else
            {
                isExpired = User.ExpireDate != null && User.ExpireDate < DateTime.Now;
            }

            return this;
        }

        public UserBroker IsPasswordEqual(string password, out bool? isEqual)
        {
            if (User == null)
            {
                isEqual = null;
            }
            else
            {
                isEqual = User.Password == password;
            }

            return this;
        }

        public UserBroker Load(string code)
        {
            return Load(code, true);
        }

        public UserBroker Load(Guid id)
        {
            return Load(id, true);
        }

        public UserBroker Load(string code, bool loadRolesAndClaimsAndModules, params string[] includes)
        {
            // Proxy enabled olduğu için Include'lara gerek yok. Ama zararı da yok.
            DbQuery<User> query = CoreEntities.Users
                .Include("Group.Branch.Company")
                .Include("Consumer");

            if (loadRolesAndClaimsAndModules)
            {
                query.Include("RoleToUserMaps.Role.Claims");
                //query.Include("RoleToUserMaps.Role.RoleToModuleMaps.Module");
            }

            includes.ToList().ForEach(x => query.Include(x));

            User = query.FirstOrDefault(x => x.Consumer.Code == code);

            return this;
        }

        public UserBroker Load(Guid id, bool loadRolesAndClaimsAndModules = false, params string[] includes)
        {
            // Proxy enabled olduğu için Include'lara gerek yok. Ama zararı da yok.
            DbQuery<User> query = CoreEntities.Users
                .Include("Group.Branch.Company")
                .Include("Consumer");

            if (loadRolesAndClaimsAndModules)
            {
                query.Include("RoleToUserMaps.Role.Claims");
                //query.Include("RoleToUserMaps.Role.RoleToModuleMaps.Module");
            }

            includes.ToList().ForEach(x => query.Include(x));

            User = query.FirstOrDefault(x => x.Id == id);

            return this;
        }

        public UserBroker Load(User user)
        {
            User = user;
            return this;
        }

        public UserBroker LoadAll()
        {
            Users = CoreEntities
                .Users
                .Include("Group.Branch.Company")
                .Where(x => x.Consumer.Code != CommonConfiguration.Configuration.DefaultUser.Username)
                .ToList();

            return this;
        }

        public UserBroker RunGroupAction(Action<GroupBroker> action)
        {
            GroupBroker groupBroker = GroupBroker.Init()
                .Load(User.GroupId);

            action.Invoke(groupBroker);

            return this;
        }

        public UserBroker SetAvailablity(bool availability)
        {
            User.Available = availability;

            return this;
        }

        public UserBroker SetExpireDate(DateTime? expireDate)
        {
            User.ExpireDate = expireDate;

            return this;
        }

        public UserBroker SetGroup(Group group)
        {
            User.Group = group;

            return this;
        }

        public UserBroker SetGroup(Guid groupId)
        {
            User.GroupId = groupId;

            return this;
        }

        public UserBroker SetPassword(string password)
        {
            User.Password = password;

            return this;
        }

        public UserBroker SetPicture(byte[] image)
        {
            User.Picture = image;

            return this;
        }
    }
}
