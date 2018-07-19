using Core.Common;
using Core.Server.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers
{
    public class CoreBaseBroker
    {
        protected CoreEntities CoreEntities { get; set; }

        public CoreBaseBroker()
        {
            CoreEntities = new CoreEntities();
            CoreEntities.Configuration.LazyLoadingEnabled = false;
            CoreEntities.Configuration.ProxyCreationEnabled = false;
        }

        public virtual void Save()
        {
            CoreEntities.SaveChanges();
        }
    }
}
