using Core.Common.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers
{
    public class RoleBroker : CoreBaseBroker
    {
        public Role Role { get; set; }
        public List<Role> Roles { get; set; }

        private RoleBroker()
        {
        }

        public RoleBroker Append()
        {
            CoreEntities.Roles.Add(Role);

            return this;
        }

        public RoleBroker CreateRole(string code, string description, bool available)
        {
            if (CoreEntities.Roles.Any(x => x.Code == code))
            {
                throw new ApplicationException($"{code} kod ile sistemde zaten rol tanımlı.");
            }

            Role = new Role()
            {
                Available = available,
                Code = code,
                Description = description
            };

            return this;
        }

        public static RoleBroker Init()
        {
            RoleBroker ret = new RoleBroker();

            return ret;
        }

        public RoleBroker LoadAllRoles()
        {
            Roles = CoreEntities.Roles.ToList();

            return this;
        }

        public RoleBroker LoadAvailableRoles()
        {
            Roles = CoreEntities.Roles.Where(x => x.Available).ToList();

            return this;
        }

        public new RoleBroker Save()
        {
            base.Save();

            return this;
        }
    }
}
