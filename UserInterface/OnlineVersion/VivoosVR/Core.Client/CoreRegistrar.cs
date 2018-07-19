using Castle.Windsor;
using Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client
{
    public class CoreRegistrar : BaseRegistrar, IRegistrar
    {
        public CoreRegistrar(IWindsorContainer container) : base(container)
        {

        }
        
    }
}
