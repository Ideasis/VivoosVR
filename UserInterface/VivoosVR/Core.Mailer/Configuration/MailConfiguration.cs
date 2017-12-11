using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core.Mail.Configuration
{
    public class MailConfiguration : ConfigurationSection
    {
        public static MailConfiguration Configuration
        {
            get
            {
                MailConfiguration ret = (MailConfiguration)ConfigurationManager.GetSection("MailConfiguration");
                return ret;
            }
        }
        [ConfigurationProperty("TemplatePath")]
        public string TemplatePath
        {
            get
            {
                return this["TemplatePath"].ToString();
            }
            set
            {
                this["TemplatePath"] = value;
            }
        }
        [ConfigurationProperty("WebAdmin")]
        public WebAdmin WebAdmin
        {
            get
            {
                return (WebAdmin)this["WebAdmin"];
            }
            set
            {
                this["WebAdmin"] = value;
            }
        }
    }
}
