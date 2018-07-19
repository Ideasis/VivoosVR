using Castle.MicroKernel;
using Castle.Windsor;
using Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.MVVM
{
    public class BaseViewModel : ReactiveObject, IBaseViewModel, IClaim
    {
        public IWindsorContainer Container { get; set; }
        public Observable<string> FullName { get; set; }
        public RxCommand LoadedCommand { get; set; }
        public string SecurityCode { get; set; }
        public Observable<string> Title { get; set; }

        public BaseViewModel()
        {
            LoadedCommand = new RxCommand();

            Title = new Observable<string>();

            Title.Subscribe(x =>
                {
                    SecurityCode = x;
                });
        }
    }
}
