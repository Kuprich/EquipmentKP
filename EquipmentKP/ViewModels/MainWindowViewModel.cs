using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly IRepository<Location> localtionsRep;
        private readonly IRepository<SubEquipmentType> subEquipmentTypesRep;
        private readonly IRepository<MainEquipmentType> mainEquipmentTypesRep;
        private readonly IRepository<MainEquipment> mainEquipmentsRep;
        private readonly IRepository<SubEquipment> subEquipmentsRep;

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

        public MainWindowViewModel(
            IRepository<Location> localtionsRep,
            IRepository<SubEquipmentType> subEquipmentTypesRep,
            IRepository<MainEquipmentType> mainEquipmentTypesRep,
            IRepository<MainEquipment> mainEquipmentsRep,
            IRepository<SubEquipment> subEquipmentsRep
            )
        {
            Title = "Главное окно модели";

            this.localtionsRep = localtionsRep;
            this.subEquipmentTypesRep = subEquipmentTypesRep;
            this.mainEquipmentTypesRep = mainEquipmentTypesRep;
            this.mainEquipmentsRep = mainEquipmentsRep;
            this.subEquipmentsRep = subEquipmentsRep;

        }
    }
}
