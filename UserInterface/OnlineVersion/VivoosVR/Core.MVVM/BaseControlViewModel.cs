using Castle.MicroKernel;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public class BaseControlViewModel : BaseViewModel, IControlViewModel
    {
        public RxCommand ControlLoadedCommand { get; set; }
        public virtual Observable<IControl> View { get; set; }
        public Observable<IWindowViewModel> WindowViewModel { get; set; }

        public BaseControlViewModel(IWindsorContainer container, IControl view, string title)
        {
            ControlLoadedCommand = new RxCommand();

            View = new Observable<IControl>();
            Title.Value = title;

            Container = container;
            View.Value = view;
            View.Value.DataContext = this;
            Title.Value = title;

            WindowViewModel = new Observable<IWindowViewModel>();
        }

        public void Close()
        {
            WindowViewModel.Value.Close();
        }
    }
}
