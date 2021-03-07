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

        private IRepository<MainEquipment> EquipmentsRep;

        private CollectionViewSource equpmentsViewSource;
        public ICollectionView EquipmentsView => equpmentsViewSource?.View;

        #region string Title - заголовок окна
        private string _Title = "ИАЦ: Движение оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        private String inventoryNoText;

        public String MyProperty
        {
            get { return inventoryNoText; }
            set { inventoryNoText = value; }
        }


        #region ObservableCollection<MainEquipment> Equipments - оборудование
        private ObservableCollection<MainEquipment> equipments;
        public ObservableCollection<MainEquipment> Equipments
        {
            get => equipments;
            set
            {
                if (!Set(ref equipments, value)) return;

                equpmentsViewSource = new CollectionViewSource { Source = value };

                equpmentsViewSource.View.Refresh();
                OnPropertyChanged(nameof(EquipmentsView));
            }
        } 
        #endregion


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

        #region LoadDataCommand - Команда загрузки данных из репозитория
        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Equipments = new ObservableCollection<MainEquipment>(await EquipmentsRep.Items.ToArrayAsync());
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription("EquipmentsKit.InventoryNo"));
        }
        #endregion

        #region GroupingCammand - Группировать (тестовая команда)
        private ICommand groupingCommand;
        public ICommand GroupingCammand => groupingCommand ?? new LambdaCommand(OnGroupingCammandExecute);
        private void OnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription("EquipmentsKit.InventoryNo"));
        }
        #endregion

        #region UnGroupingCammand - Разгруппировать
        private ICommand unGroupingCommand;
        public ICommand UnGroupingCammand => unGroupingCommand ?? new LambdaCommand(OnUnGroupingCammandExecute);
        private void OnUnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
        } 
        #endregion

        #endregion

        public MainWindowViewModel(IRepository<MainEquipment> EquipmentsRep)
        {
            this.EquipmentsRep = EquipmentsRep;
            equpmentsViewSource = new CollectionViewSource { Source = Equipments };

            //var kit = new EquipmentsKit { InventoryNum = "000111000111", Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };

            //EquipmentsKitRep.Add(kit);
            //{ InventoryNum = "021384123", Location = locations[2], Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };
        }

        public MainWindowViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
    }
}
