using Castle.MicroKernel.ComponentActivator;
using Castle.Windsor;
using Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client
{
    public abstract class BaseRegistrar : IRegistrar
    {
        public IWindsorContainer Container { get; set; }

        public BaseRegistrar(IWindsorContainer container)
        {
            Container = container;
        }
    }
}
