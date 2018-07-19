using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace ServerBase.Library
{
    public abstract  class BaseUserBroker
    {
        User User { get; set; }
        List<User> Users { get; set; }

        public abstract BaseUserBroker Append();
        public abstract BaseUserBroker CreateUser(PrimitiveUserDto user);
        public abstract BaseUserBroker DeleteUser();
        public abstract PrimitiveUserDto GetAsPrimiviteUserDto();
        public abstract IEnumerable<Claim> GetClaims();
        public abstract IEnumerable<RoleToUserMap> GetRoleMaps();
        public abstract IEnumerable<Role> GetRoles();
        public abstract BaseUserBroker IsExpired(out bool? isExpired);
        public abstract BaseUserBroker IsPasswordEqual(string password, out bool? isEqual);
        public abstract BaseUserBroker Load(User user);
        public abstract BaseUserBroker Load(string code);
        public abstract BaseUserBroker Load(Guid id);
        public abstract BaseUserBroker Load(string code, bool loadRolesAndClaimsAndModules, params string[] includes);
        public abstract BaseUserBroker Load(Guid id, bool loadRolesAndClaimsAndModules = false, params string[] includes);
        public abstract BaseUserBroker LoadAll();
        public abstract BaseUserBroker ModifyUser(PrimitiveUserDto user);
        public abstract BaseUserBroker RunGroupAction(Action<GroupBroker> action);
        public abstract BaseUserBroker SetAvailablity(bool availability);
        public abstract BaseUserBroker SetExpireDate(DateTime? expireDate);
        public abstract BaseUserBroker SetGroup(Guid groupId);
        public abstract BaseUserBroker SetGroup(Group group);
        public abstract BaseUserBroker SetPassword(string password);
        public abstract BaseUserBroker SetPicture(byte[] image);
    }
}