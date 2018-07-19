using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Vivos.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class SearchControlViewModel : BaseControlViewModel, ISearchControlViewModel
    {
        public Observable<string> Criteria { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OpenCommand { get; set; }
        public RxCommand SearchCommand { get; set; }
        public Observable<string> SearchResult { get; set; }

        public SearchControlViewModel(IWindsorContainer container, ISearchControl view)
            : base(container, view, "Ögeler")
        {
            IsRunning = new Observable<bool>(false);

            
            
        }
    }
}
