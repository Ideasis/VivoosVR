using Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public interface IControlViewModel : IBaseViewModel, IClaim
    {
        RxCommand ControlLoadedCommand { get; set; }
        Observable<IControl> View { get; set; }
        Observable<IWindowViewModel> WindowViewModel { get; set; }

        void Close();
    }
}
