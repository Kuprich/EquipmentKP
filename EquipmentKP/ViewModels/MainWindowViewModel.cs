using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА

        #region string Title - заголовок окна
        private string _Title = "ИАЦ: Движение оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        //private ObservableCollection<EquipmentsKit> equipmentsKits;

        public ObservableCollection<EquipmentsKit> EquipmentsKits => (ObservableCollection<EquipmentsKit>)EquipmentsKitRep.Items;


        #endregion

        #region КОМАНДЫ
        #region CloseAplicationCommand - Команда закрытия окна
        private ICommand _CloseAplicationCommand;
        public ICommand CloseAplicationCommand => _CloseAplicationCommand ??= new LambdaCommand(OnCloseAplicationCommandExecuted);
        //private bool CanCloseAplicationCommandExecute() => true; // если этого параметра нет, то всегда разрешено выполнение данной команды
        private void OnCloseAplicationCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region TestAsyncCommand - пример асинхронной команды
        private ICommand _TestAsyncCommand;
        public ICommand TestAsyncCommand => _TestAsyncCommand ??= new LambdaCommandAsync(OnTestAsyncCommandExecuted);
        private DateTime _Time;
        private readonly IRepository<EquipmentsKit> EquipmentsKitRep;

        public DateTime Time
        {
            get => _Time;
            set => Set(ref _Time, value);
        }

        private async Task OnTestAsyncCommandExecuted(object p) => await Task.Run(TestAsyncMethod);

        private void TestAsyncMethod()
        {
            Thread.Sleep(5000);
            Time = DateTime.Now;
        }
        #endregion 
        #endregion

        public MainWindowViewModel(IRepository<EquipmentsKit> EquipmentsKitRep)
        {
            this.EquipmentsKitRep = EquipmentsKitRep;
            //EquipmentsKits = (ObservableCollection<EquipmentsKit>) EquipmentsKitRep.Items;
        }
    }
}
