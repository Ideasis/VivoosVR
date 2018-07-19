using Castle.MicroKernel;
using Castle.Windsor;
using Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public interface IWindowViewModel : IBaseViewModel
    {
        Observable<bool> CanClose { get; set; }
        RxCommand ClosingCommand { get; set; }
        Observable<IControlViewModel> CurrentControlViewModel { get; set; }
        Observable<bool> IsEnabled { get; set; }
        RxCommand LoadedCommand { get; set; }
        IControlViewModel SecurityFailedControlViewModel { get; set; }
        Observable<IWindow> Window { get; set; }

        void Close();

        TControlViewModel GetContent<TControlViewModel, TControl>(bool changeTitle)
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl;

        TControlViewModel GetContent<TControlViewModel, TControl>(bool changeTitle, params Arg[] args)
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl;

        TControlViewModel LoadContent<TControlViewModel, TControl>()
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl;

        TControlViewModel LoadContent<TControlViewModel, TControl>(params Arg[] args)
                    where TControlViewModel : IControlViewModel, IClaim
                    where TControl : IControl;

        TWindow LoadWindow<TWindowViewModel, TWindow>(TWindowViewModel windowViewModel)
                    where TWindowViewModel : class, IWindowViewModel, IClaim
                    where TWindow : class, IWindow;
    }
}
