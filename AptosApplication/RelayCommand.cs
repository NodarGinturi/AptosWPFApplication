using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AptosApplication
{
    public class RelayCommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public RelayCommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                //throw new NotImplementedException();
            }

            remove
            {
                //throw new NotImplementedException();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
                return _TargetCanExecuteMethod();
            if (_TargetExecuteMethod != null)
                return true;
            return false;
        }

        private event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
                 _TargetCanExecuteMethod();
        }
    }
}
