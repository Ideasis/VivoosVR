using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Configuration
{
    public class MarketConfiguration : ConfigurationSection
    {
        public static MarketConfiguration Configuration
        {
            get
            {
                MarketConfiguration ret = (MarketConfiguration)ConfigurationManager.GetSection("MarketConfiguration");
                return ret;
            }
        }
    }
}
