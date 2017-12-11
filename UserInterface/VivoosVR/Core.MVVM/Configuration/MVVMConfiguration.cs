using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM.Configuration
{
    public class MVVMConfiguration : ConfigurationSection
    {
        public static MVVMConfiguration Configuration
        {
            get
            {
                MVVMConfiguration ret = (MVVMConfiguration)ConfigurationManager.GetSection("MVVMConfiguration");
                return ret;
            }
        }
        [ConfigurationProperty("DefaultTitle", DefaultValue = "")]
        public string DefaultTitle
        {
            get
            {
                return this["DefaultTitle"].ToString();
            }
            set
            {
                this["DefaultTitle"] = value;
            }
        }
        [ConfigurationProperty("MessageBoxTitle", DefaultValue = "")]
        public string MessageBoxTitle
        {
            get
            {
                return this["MessageBoxTitle"].ToString();
            }
            set
            {
                this["MessageBoxTitle"] = value;
            }
        }
    }
}
