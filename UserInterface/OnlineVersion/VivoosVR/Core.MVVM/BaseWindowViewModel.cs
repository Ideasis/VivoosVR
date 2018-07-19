using Castle.MicroKernel;
using Castle.Windsor;
using Core.Log;
using Core.Log.Exceptions;
using Core.MVVM.Configuration;
using Core.MVVM.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.MVVM
{
    public class BaseWindowViewModel : BaseViewModel, IWindowViewModel
    {
        public Observable<bool> CanClose { get; set; }
        public RxCommand ClosingCommand { get; set; }
        public Observable<IControlViewModel> CurrentControlViewModel { get; set; }
        public Observable<bool> IsEnabled { get; set; }
        public ISecurityChecker SecurityChecker { get; set; }
        public IControlViewModel SecurityFailedControlViewModel { get; set; }
        public Observable<IWindow> Window { get; set; }

        public BaseWindowViewModel(IWindsorContainer container, IWindow window, ISecurityChecker securityChecker, IControlViewModel securityFailedControlViewModel)
        {
            SecurityFailedControlViewModel = securityFailedControlViewModel;
            SecurityChecker = securityChecker;
            Window = new Observable<IWindow>();
            IsEnabled = new Observable<bool>(true);
            Window.Value = window;
            Window.Value.DataContext = this;
            Container = container;
            CurrentControlViewModel = new Observable<IControlViewModel>();
            ClosingCommand = new RxCommand();
            CanClose = new Observable<bool>(true);

            IsEnabled.Subscribe(x =>
                {
                    Window.Value.IsEnabled = x;
                });


        }

        public void Close()
        {
            Window.Value.Close();
        }

        public TControlViewModel GetContent<TControlViewModel, TControl>(bool changeTitle)
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl
        {
            TControlViewModel ret;

            bool isAllowed = SecurityChecker.AmIAllowed<TControlViewModel>();

            ret = Container.Resolve<TControlViewModel>();

            if (!isAllowed)
            {
                throw SecurityException.CreateCoreException<TControlViewModel>(ret);
            }

            ret.WindowViewModel.Value = this;

            ret.ControlLoadedCommand.Execute(ret);

            if (changeTitle) Title.Value = string.Format("{0} {1}", MVVMConfiguration.Configuration.DefaultTitle, ((IBaseViewModel)ret).Title != null ? string.Format("- {0}", ((IBaseViewModel)ret).Title) : "");

            return ret;
        }

        public TControlViewModel GetContent<TControlViewModel, TControl>(bool changeTitle, params Arg[] args)
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl
        {
            TControlViewModel ret;

            Dictionary<string, object> parameterDictionary = args.ToDictionary(x => x.Name, x => x.Value);

            Arguments castleArgs = new Arguments(parameterDictionary);

            bool isAllowed = SecurityChecker.AmIAllowed<TControlViewModel>();

            ret = Container.Resolve<TControlViewModel>(parameterDictionary);

            if (!isAllowed)
            {
                throw SecurityException.CreateCoreException<TControlViewModel>(ret);
            }

            ret.WindowViewModel.Value = this;

            ret.ControlLoadedCommand.Execute(ret);

            if (changeTitle) Title.Value = string.Format("{0} {1}", MVVMConfiguration.Configuration.DefaultTitle, ((IBaseViewModel)ret).Title != null ? string.Format("- {0}", ((IBaseViewModel)ret).Title) : "");

            return ret;
        }

        public TControlViewModel LoadContent<TControlViewModel, TControl>()
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl
        {

            bool isAllowed = SecurityChecker.AmIAllowed<TControlViewModel>();
            //TControlViewModel controlViewModel = Container.Resolve<TControlViewModel>();

            TControlViewModel controlViewModel = GetContent<TControlViewModel, TControl>(true);

            if (isAllowed)
            {
                Title.Value = string.Format("{0} {1}", MVVMConfiguration.Configuration.DefaultTitle, ((IClaim)controlViewModel).SecurityCode != null ? string.Format("- {0}", ((IClaim)controlViewModel).SecurityCode) : "");
                CurrentControlViewModel.Value = controlViewModel;

                CurrentControlViewModel.Value.WindowViewModel.Value = this;

                //controlViewModel.ControlLoadedCommand.Execute(controlViewModel);

                return (TControlViewModel)CurrentControlViewModel.Value;
            }
            else
            {
                throw SecurityException.CreateCoreException<TControlViewModel>(controlViewModel);
            }


        }

        public TControlViewModel LoadContent<TControlViewModel, TControl>(params Arg[] args)
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl
        {
            bool isAllowed = SecurityChecker.AmIAllowed<TControlViewModel>();

            TControlViewModel controlViewModel = GetContent<TControlViewModel, TControl>(true, args);

            if (isAllowed)
            {
                CurrentControlViewModel.Value = controlViewModel;

                CurrentControlViewModel.Value.WindowViewModel.Value = this;

                //controlViewModel.ControlLoadedCommand.Execute(controlViewModel);

                return (TControlViewModel)CurrentControlViewModel.Value;
            }
            else
            {
                throw SecurityException.CreateCoreException<TControlViewModel>(controlViewModel);
            }


        }

        public TWindow LoadWindow<TWindowViewModel, TWindow>(TWindowViewModel windowViewModel)
                    where TWindowViewModel : class, IWindowViewModel, IClaim
                    where TWindow : class, IWindow
        {
            bool isAllowed = SecurityChecker.AmIAllowed<TWindowViewModel>();

            if (isAllowed)
            {
                Window.Value = (IWindow)Container.Resolve<TWindow>();
                Window.Value.DataContext = this;
                Window.Value.Show();
                LoadedCommand.Execute(null);

                return (TWindow)Window.Value;
            }
            else
            {
                throw SecurityException.CreateCoreException<TWindowViewModel>(windowViewModel);
            }
        }
    }
}
