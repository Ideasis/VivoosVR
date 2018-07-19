using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class AdminHomeControlViewModel : BaseControlViewModel, IAdminHomeControlViewModel
    {
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OpenAssetList { get; set; }
        public RxCommand OpenNewAsset { get; set; }

        public AdminHomeControlViewModel(IWindsorContainer container, IAdminHomeControl view)
                    : base(container, view, "Sistem Yöneticisi")
        {
            IsRunning = new Observable<bool>(false);
            OpenNewAsset = new RxCommand();
            OpenAssetList = new RxCommand();

            OpenNewAsset.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IAssetEntryControlViewModel, IAssetEntryControl>();
                });

            OpenAssetList.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IAssetListControlViewModel, IAssetListControl>();
                });

            
            
        }
    }
}
