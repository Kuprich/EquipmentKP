using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Title - заголовок окна
        private string _Title;

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion


        // пример синхронной команды
        #region CloseAplicationCommand - Команда закрытия окна
        private ICommand _CloseAplicationCommand;
        public ICommand CloseAplicationCommand => _CloseAplicationCommand ??= new LambdaCommand(OnCloseAplicationCommandExecuted);
        //private bool CanCloseAplicationCommandExecute() => true; // если этого параметра нет, то всегда разрешено выполнение данной команды
        private void OnCloseAplicationCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #endregion

        private ICommand _TestAsyncCommand;
        public ICommand TestAsyncCommand => _TestAsyncCommand ??= new LambdaCommandAsync(OnTestAsyncCommandExecuted);
        private DateTime _Time;

        public DateTime Time
        {
            get => _Time;
            set => Set(ref _Time, value);
        }

        private async Task OnTestAsyncCommandExecuted(object p) => await Task.Run(TestAsyncMethod);

        private async Task TestAsyncMethod()
        {
            Thread.Sleep(5000);
            Time = DateTime.Now;
        }

        public MainWindowViewModel()
        {
            Title = "Главное окно модели";
        }
    }
}
