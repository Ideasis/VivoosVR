using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core.Log.Configuration
{
    public class LogConfiguration : ConfigurationSection
    {
        public static LogConfiguration Configuration
        {
            get
            {
                LogConfiguration ret = (Configuration.LogConfiguration)ConfigurationManager.GetSection("LogConfiguration");
                return ret;
            }
        }
        [ConfigurationProperty("PlainTextFileFormat", DefaultValue = "LOG_{0:yyyy_MM_dd_HH_mm_ss_fff}.txt")]
        public string PlainTextFileFormat
        {
            get
            {
                return this["PlainTextFileFormat"].ToString();
            }
            set
            {
                this["PlainTextFileFormat"] = value;
            }
        }
        [ConfigurationProperty("SecurityExceptionMessage")]
        public string SecurityExceptionMessage
        {
            get
            {
                return this["SecurityExceptionMessage"].ToString();
            }
            set
            {
                this["SecurityExceptionMessage"] = value;
            }
        }
        [ConfigurationProperty("Source")]
        public string Source
        {
            get
            {
                return this["Source"].ToString();
            }
            set
            {
                this["Source"] = value;
            }
        }
        [ConfigurationProperty("SystemExceptionMessage")]
        public string SystemExceptionMessage
        {
            get
            {
                return this["SystemExceptionMessage"].ToString();
            }
            set
            {
                this["SystemExceptionMessage"] = value;
            }
        }
        [ConfigurationProperty("UserWindowSource", IsRequired = false)]
        public string UserWindowSource
        {
            get
            {
                return this["UserWindowSource"].ToString();
            }
            set
            {
                this["UserWindowSource"] = value;
            }
        }
        [ConfigurationProperty("XmlFileFormat", DefaultValue = "LOG_{0:yyyy_MM_dd_HH_mm_ss_fff}.svclog")]
        public string XmlFileFormat
        {
            get
            {
                return this["XmlFileFormat"].ToString();
            }
            set
            {
                this["XmlFileFormat"] = value;
            }
        }
    }
}
