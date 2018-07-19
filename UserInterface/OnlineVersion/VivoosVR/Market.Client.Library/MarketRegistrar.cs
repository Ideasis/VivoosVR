using Core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using System.Diagnostics;
using Core.Log;
using Castle.MicroKernel.Registration;
using Market.Client.Models;

namespace Market.Client
{
    public class MarketRegistrar : BaseRegistrar, IRegistrar
    {
        public MarketRegistrar(IWindsorContainer container) : base(container)
        {

        }
    }
}
