using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.MVVM
{
    public class RxCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Func<object, bool> CanExecuteFunction { get; set; }
        public Action<object> ExecuteAction { get; set; }

        public RxCommand()
        {
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
                ExecuteAction(parameter);
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
