using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core.Common.Configuration
{
    public class CommonConfiguration : ConfigurationSection
    {
        #region Properties

        public static CommonConfiguration Configuration
        {
            get
            {
                CommonConfiguration ret = (CommonConfiguration)ConfigurationManager.GetSection("CommonConfiguration");
                return ret;
            }
        }

        [ConfigurationProperty("DefaultUser")]
        public DefaultUser DefaultUser
        {
            get
            {
                return (DefaultUser)this["DefaultUser"];
            }
            set
            {
                this["DefaultUser"] = value;
            }
        }

        

        #endregion Properties
    }
}