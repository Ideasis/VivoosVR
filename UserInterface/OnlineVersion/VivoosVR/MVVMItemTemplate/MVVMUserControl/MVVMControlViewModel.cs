using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using UI.Doctor.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$
{
    public class $safeitemname$ : BaseControlViewModel, I$safeitemname$
    {
        public Observable<bool> IsRunning { get; set; }
		public Observable<bool> IsOperationsVisible { get; set; }
        
        public $safeitemname$(IWindsorContainer container, I$fileinputname$Control view)
            : base(container, view, "$safeitemname$")
        {
            IsRunning = new Observable<bool>(false);
            IsOperationsVisible = new Observable<bool>(false);
			

            RxMessageBus.Subscribe<OperationsMessage>(x =>
            {
                IsOperationsVisible.Value = !IsOperationsVisible.Value;
            });
            
        }
    }
}
