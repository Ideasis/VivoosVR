using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client
{
    public class Bootstrapper
    {
        private static IWindsorContainer _Container = null;
        static Bootstrapper()
        {
            Trace.WriteLine("** BaseBootStrapper called.");
            _Container = new WindsorContainer();
            Init();
        }

        public static T Resolve<T>()
        {
            return _Container.Resolve<T>();
        }

        public static ISession ResolveSession()
        {
            ISession ret = Resolve<ISession>();

            return ret;
        }

        public static void Init()
        {
            AssemblyFilter assemblyFilter = new AssemblyFilter(AppDomain.CurrentDomain.BaseDirectory);

            _Container.Register(
                Component.For<IWindsorContainer>().Instance(_Container).LifestyleSingleton(),
                Classes.FromAssemblyContaining(typeof(ISession)).BasedOn<ISession>().WithService.FromInterface(typeof(ISession)).LifestyleSingleton(),
                Classes.FromAssemblyInDirectory(assemblyFilter).AllowMultipleMatches().BasedOn<ISecurityChecker>().WithService.FromInterface(typeof(ISecurityChecker)).LifestyleSingleton(),

                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IControl>().WithService.FromInterface(typeof(IControl)).LifestyleTransient(), // transient must come before singletons !!!
                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IWindow>().WithService.FromInterface(typeof(IWindow)).LifestyleSingleton(),

                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IControlViewModel>().WithService.FromInterface(typeof(IControlViewModel)).LifestyleTransient(), // transient must come before singletons !!!
                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IWindowViewModel>().WithService.FromInterface(typeof(IWindowViewModel)).LifestyleSingleton(),

                Classes.FromAssemblyInDirectory(assemblyFilter).BasedOn<IRegistrar>().WithService.FromInterface(typeof(IRegistrar)).LifestyleSingleton().AllowMultipleMatches()
                );

            var otherRegisterars = _Container.ResolveAll<IRegistrar>(new Dictionary<string, IWindsorContainer>() { { "container", _Container } }).ToList();

            Console.WriteLine("** Dumping bootstrapper :");
            foreach (var handler in _Container.Kernel.GetAssignableHandlers(typeof(object)))
            {
                foreach (var service in handler.ComponentModel.Services)
                {
                    Console.WriteLine("** {0} => {1}", service.FullName, handler.ComponentModel.Implementation.FullName);
                }
            }

            otherRegisterars.ForEach(x => Console.WriteLine($"IRegistrar: {x.GetType().FullName}"));

            //otherRegisterars.ForEach(x => x.Init());
        }
    }
}
