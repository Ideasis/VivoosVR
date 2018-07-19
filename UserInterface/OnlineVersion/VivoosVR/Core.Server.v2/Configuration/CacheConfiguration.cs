using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Configuration
{
    public class CacheConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("ExpirePeriod", DefaultValue = "00:05:00", IsRequired = true)]
        public TimeSpan ExpirePeriod
        {
            get
            {
                return TimeSpan.Parse(this["ExpirePeriod"].ToString());
            }
            set
            {
                this["ExpirePeriod"] = value;
            }
        }
    }
}
