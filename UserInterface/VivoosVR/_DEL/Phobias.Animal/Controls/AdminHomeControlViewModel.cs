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

namespace Phobias.Animal.Controls
{
    public class GovernanceControlViewModel : BaseControlViewModel, IGovernanceControlViewModel
    {
        public Observable<string> Criteria { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OpenCommand { get; set; }
        public RxCommand SearchCommand { get; set; }
        public Observable<string> SearchResult { get; set; }

        public GovernanceControlViewModel(IWindsorContainer container, IGovernanceControl view)
            : base(container, view, "HAYVAN / BÖCEK FOBİSİ")
        {
            IsRunning = new Observable<bool>(false);

            
            
        }
    }
}
