using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.Infrastructure.Commands
{
    class DialogResultCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool? DialogResult { get; set; }

        public bool CanExecute(object parameter) => App.ActiveWindow != null;

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = App.CurrentWindow;
            var dialogResult = DialogResult;

            if (parameter != null)
                dialogResult = Convert.ToBoolean(parameter);

            window.DialogResult = dialogResult;
            window.Close();
        }
    }
}
