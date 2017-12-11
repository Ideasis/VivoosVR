using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Client;
using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos
{
    public class VivosRegistrar : BaseRegistrar
    {
        public VivosRegistrar() : base(new WindsorContainer())
        {
        }
    }
}
