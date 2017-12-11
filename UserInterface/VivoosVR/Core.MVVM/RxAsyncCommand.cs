using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Core.MVVM
{
    public class RxAsyncCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Func<object, bool> CanExecuteFunction { get; set; }
        public DispatcherPriority DispatcherPriority { get; set; }
        public Action<object> ExecuteAction { get; set; }

        public RxAsyncCommand(DispatcherPriority dispatcherPriority = DispatcherPriority.Normal)
        {
            DispatcherPriority = dispatcherPriority;

            CanExecuteChanged += RxCommand_CanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunction == null ? true : CanExecuteFunction(parameter);
        }

        public void Execute(object parameter)
        {
            if (ExecuteAction != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() =>
                {
                    ExecuteAction.Invoke(parameter);
                }), DispatcherPriority);
            }
        }

        void RxCommand_CanExecuteChanged(object sender, EventArgs e)
        {

        }

        public void Subscribe(Action<object> executeAction, Func<object, bool> canExecuteFunction = null)
        {
            ExecuteAction = executeAction;
            CanExecuteFunction = canExecuteFunction;
        }
    }
}
