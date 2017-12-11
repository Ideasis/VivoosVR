using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Configuration
{
    public class DoctorConfiguration : ConfigurationSection
    {
        public static DoctorConfiguration Configuration
        {
            get
            {
                DoctorConfiguration ret = (DoctorConfiguration)ConfigurationManager.GetSection("DoctorConfiguration");
                return ret;
            }
        }

        
    }
}
