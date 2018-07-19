using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core.Server.Configuration
{
    public class ServerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("CacheConfiguration")]
        public CacheConfiguration CacheConfiguration
        {
            get
            {
                return (CacheConfiguration)this["CacheConfiguration"];
            }
            set
            {
                this["CacheConfiguration"] = value;
            }
        }
        public static ServerConfiguration Configuration
        {
            get
            {
                ServerConfiguration ret = (ServerConfiguration)ConfigurationManager.GetSection("ServerConfiguration");
                return ret;
            }
        }
    }
}
