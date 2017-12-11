using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core.Client.Configuration
{
    public class ClientConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("AdminRoleCode", DefaultValue = "AdminRole")]
        public string AdminRoleCode
        {
            get
            {
                return this["AdminRoleCode"].ToString();
            }
            set
            {
                this["AdminRoleCode"] = value;
            }
        }
        public static ClientConfiguration Configuration
        {
            get
            {
                ClientConfiguration ret = (ClientConfiguration)ConfigurationManager.GetSection("ClientConfiguration");
                return ret;
            }
        }
        [ConfigurationProperty("DoctorRole", DefaultValue = "DoctorRole")]
        public string DoctorRole
        {
            get
            {
                return this["DoctorRole"].ToString();
            }
            set
            {
                this["DoctorRole"] = value;
            }
        }
        [ConfigurationProperty("MaxImageSizeInKB", DefaultValue = 200)]
        public int MaxImageSizeInKB
        {
            get
            {
                return Convert.ToInt32(this["MaxImageSizeInKB"]);
            }
            set
            {
                this["MaxImageSizeInKB"] = value;
            }
        }
    }
}
