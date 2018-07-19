using Market.Client.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.MVVM;
using Castle.Windsor;
using Phobias.Animal.Controls;

namespace Market.Client.Phobias.Animal
{
    public class StartUp : IStartUp
    {
        public IWindsorContainer Container { get; set; }

        public StartUp(IWindsorContainer cocntainer)
        {
            Container = Container;
        }

        public IControlViewModel GetStartUpControl()
        {
            IGovernanceControlViewModel ret = null;
            ret = Container.Resolve<IGovernanceControlViewModel>();

            return ret;
        }

        public string GetWelcomeMessage()
        {
            return "Hayvan ve böcek fobilerine yönelik maruz bırakma uygulamasına hoşgeldiniz.";
        }

        public Guid GetAssetKey()
        {
            return new Guid("32E84DA5-E4EE-4BFB-BB93-9E0D9BDF4123");
        }
    }
}
