using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
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

        private IRepository<EquipmentsKit> EquipmentsKitRep;

        #region string Title - заголовок окна
        private string _Title = "ИАЦ: Движение оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion


        private ObservableCollection<EquipmentsKit> equipmentsKit;
        public ObservableCollection<EquipmentsKit> EquipmentsKits
        {
            get => equipmentsKit;
            set => Set(ref equipmentsKit, value);
        }

        private EquipmentsKit selectedKit;
        public EquipmentsKit SelectedKit
        {
            get => selectedKit;
            set => Set(ref selectedKit, value);
        }



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

        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            EquipmentsKits = new ObservableCollection<EquipmentsKit>(await EquipmentsKitRep.Items.ToArrayAsync());
        }

        #endregion

        public MainWindowViewModel(IRepository<EquipmentsKit> EquipmentsKitRep)
        {
            this.EquipmentsKitRep = EquipmentsKitRep;
            
            //var kit = new EquipmentsKit { InventoryNum = "000111000111", Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };

            //EquipmentsKitRep.Add(kit);
            //{ InventoryNum = "021384123", Location = locations[2], Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };
        }

        public MainWindowViewModel()
        {

        }
    }
}
