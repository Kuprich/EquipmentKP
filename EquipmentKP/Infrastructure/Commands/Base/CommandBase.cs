using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.Infrastructure.Commands.Base
{
    abstract class CommandBase : ICommand
    {
        private bool _Executable = true;
        public bool Executable
        {
            get => _Executable;
            set
            {
                if (_Executable == value) return;
                _Executable = value;
                ExecutableChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler ExecutableChanged;

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        protected virtual bool CanExecute(object parameter) => true;
        bool ICommand.CanExecute(object parameter) => Executable && CanExecute(parameter);

        protected abstract void Execute(object parameter);
        void ICommand.Execute(object parameter)
        {
            if (!((ICommand)this).CanExecute(parameter)) return;
            Execute(parameter);
        }





    }
}
