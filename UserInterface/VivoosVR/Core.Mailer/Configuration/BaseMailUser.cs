using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace Core.Mail.Configuration
{
    public abstract class BaseMailUser : ConfigurationElement
    {
        [ConfigurationProperty("EMail", DefaultValue = "", IsRequired = true)]
        public string Email
        {
            get
            {
                return this["EMail"].ToString();
            }
            set
            {
                this["EMail"] = value;
            }
        }
        [ConfigurationProperty("Name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return this["Name"].ToString();
            }
            set
            {
                this["Name"] = value;
            }
        }
        [ConfigurationProperty("Port", DefaultValue = 25)]
        public int Port
        {
            get
            {
                return Convert.ToInt32(this["Port"], CultureInfo.CurrentCulture.NumberFormat);
            }
            set
            {
                this["Port"] = value;
            }
        }
        [ConfigurationProperty("Server", DefaultValue = "", IsRequired = true)]
        public string Server
        {
            get
            {
                return this["Server"].ToString();
            }
            set
            {
                this["Server"] = value;
            }
        }
        public NetworkCredential UserCredential
        {
            get
            {
                return new NetworkCredential(WindowsIdentityName, WindowsPassword);
            }
        }
        [ConfigurationProperty("WindowsIdentityName", DefaultValue = "", IsRequired = true)]
        public string WindowsIdentityName
        {
            get
            {
                return this["WindowsIdentityName"].ToString();
            }
            set
            {
                this["WindowsIdentityName"] = value;
            }
        }
        [ConfigurationProperty("WindowsPassword", DefaultValue = "", IsRequired = true)]
        public string WindowsPassword
        {
            get
            {
                return this["WindowsPassword"].ToString();
            }
            set
            {
                this["WindowsPassword"] = value;
            }
        }
    }
}
