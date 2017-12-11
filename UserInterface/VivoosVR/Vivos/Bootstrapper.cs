using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.MVVM;
using Vivos.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Client;
using System.Diagnostics;
using Castle.MicroKernel.ComponentActivator;

namespace Vivos
{
    public static class Bootstrapper
    {
        public static IWindsorContainer Container { get; set; }

        public static void Init()
        {
            Container = new WindsorContainer();
            AssemblyFilter assemblyFilter = new AssemblyFilter(AppDomain.CurrentDomain.BaseDirectory);

            Container.Register(
                Component.For<IWindsorContainer>().Instance(Container).LifestyleSingleton(),
                Classes.FromAssemblyContaining(typeof(ISession)).BasedOn<ISession>().WithService.FromInterface(typeof(ISession)).LifestyleSingleton(),
                //Classes.FromAssemblyInDirectory(assemblyFilter).AllowMultipleMatches().BasedOn<IVisual>().WithService.FromInterface(typeof(IVisual)).LifestyleSingleton(),
                Classes.FromAssemblyInDirectory(assemblyFilter).AllowMultipleMatches().BasedOn<ISecurityChecker>().WithService.FromInterface(typeof(ISecurityChecker)).LifestyleSingleton(),
                //Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IBaseViewModel>().WithService.FromInterface(typeof(IBaseViewModel)).LifestyleSingleton(),

                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IControl>().WithService.FromInterface(typeof(IControl)).LifestyleTransient(), // transient must come before singletons !!!
                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IWindow>().WithService.FromInterface(typeof(IWindow)).LifestyleSingleton(),

                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IControlViewModel>().WithService.FromInterface(typeof(IControlViewModel)).LifestyleTransient(), // transient must come before singletons !!!
                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IWindowViewModel>().WithService.FromInterface(typeof(IWindowViewModel)).LifestyleSingleton(),
                
                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IRegistrar>().WithService.FromInterface(typeof(IRegistrar)).LifestyleSingleton().AllowMultipleMatches()
                );

            Console.WriteLine("** Dumping bootstrapper :");
            foreach (var handler in Container.Kernel.GetAssignableHandlers(typeof(object)))
            {
                foreach (var service in handler.ComponentModel.Services)
                {
                    Console.WriteLine("** {0} => {1}", service.FullName, handler.ComponentModel.Implementation.FullName);
                }
            }

            var otherRegisterars = Container.ResolveAll<IRegistrar>().ToList();

            otherRegisterars.ForEach(x => x.Init(Container));
        }

        public static T Resolve<T>() 
        {
            T ret = default(T);

            try
            {
                ret = Container.Resolve<T>();
            }
            catch (ComponentActivatorException)
            {
                
            }

            return ret;
        }

        public static ISession ResolveSession()
        {
            ISession ret = Bootstrapper.Resolve<ISession>();

            return ret;
        }
    }
}
