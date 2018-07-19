using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Configuration
{
    public class DefaultUser : ConfigurationElement
    {
        [ConfigurationProperty("Username")]
        public string Username
        {
            get
            {
                return this["Username"].ToString();
            }
            set
            {
                this["Username"] = value;
            }
        }

        [ConfigurationProperty("Password")]
        public string Password
        {
            get
            {
                return this["Password"].ToString();
            }
            set
            {
                this["Password"] = value;
            }
        }
    }
}
