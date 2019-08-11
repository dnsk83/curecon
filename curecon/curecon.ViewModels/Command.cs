using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace curecon.ViewModels
{

    public class Command : ICommand
    {
        private Action p;

        public Command(Action p)
        {
            this.p = p;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            p?.Invoke();
        }
    }
}
