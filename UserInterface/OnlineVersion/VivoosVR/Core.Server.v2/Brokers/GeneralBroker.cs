using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers
{
    public class GeneralBroker : CoreBaseBroker
    {
        private GeneralBroker()
        {
        }

        public static GeneralBroker Init()
        {
            GeneralBroker ret = new GeneralBroker();

            return ret;
        }
    }
}
