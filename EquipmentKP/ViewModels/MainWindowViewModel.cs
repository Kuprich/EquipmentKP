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

        #region String InventoryNo - поле для фильтра
        private String inventoryNoFilter;

        public String InventoryNoFilter
        {
            get => inventoryNoFilter;
            set
            {
                if (!Set(ref inventoryNoFilter, value)) return;

                equipmentsViewSource.View.Refresh();
            }
        }
        #endregion

        #region View & ViewSource equipments (А так же фильтр)
        private CollectionViewSource equipmentsViewSource;
        public ICollectionView EquipmentsView => equipmentsViewSource?.View;
        private void EquipmentsViewSource_Filter(object sender, FilterEventArgs e)
        {
            if ( !(e.Item is MainEquipment equipment) || string.IsNullOrEmpty(InventoryNoFilter) ) return;

            if ( !equipment.EquipmentsKit.InventoryNo.Contains(InventoryNoFilter.Trim(), StringComparison.OrdinalIgnoreCase) )
                e.Accepted = false;
        } 
        #endregion

        #region string Title - заголовок окна
        private string _Title = "ИАЦ: Движение оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region ObservableCollection<MainEquipment> Equipments - оборудование
        private ObservableCollection<MainEquipment> equipments;
        public ObservableCollection<MainEquipment> Equipments
        {
            get => equipments;
            set
            {
                if (!Set(ref equipments, value)) return;

                equipmentsViewSource = new CollectionViewSource { Source = value };

                equipmentsViewSource.View.Refresh();
                OnPropertyChanged(nameof(EquipmentsView));
            }
        }
        #endregion

        #region MainEquipment SelectedEquipment - выбранное оборудование
        private MainEquipment selectedEquipment;

        public MainEquipment SelectedEquipment
        {
            get => selectedEquipment;
            set => Set(ref selectedEquipment, value);
        }
        #endregion

        #region Request SelectedRequest - выбранная заявка
        private Request selectedRequest;

        public Request SelectedRequest
        {
            get => selectedRequest;
            set => Set(ref selectedRequest, value);
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
        private ICommand loadDataCommand;
        public ICommand LoadDataCommand => loadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Equipments = new ObservableCollection<MainEquipment>(await EquipmentsRep.Items.ToArrayAsync());

            equipmentsViewSource.Filter += EquipmentsViewSource_Filter;
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription($"{nameof(EquipmentsKit)}.{nameof(EquipmentsKit.InventoryNo)}"));

        }
        #endregion

        #region GroupingCammand - Группировать (тестовая команда)
        private ICommand groupingCommand;
        public ICommand GroupingCammand => groupingCommand ?? new LambdaCommand(OnGroupingCammandExecute);
        private void OnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription($"{nameof(EquipmentsKit)}.{nameof(EquipmentsKit.InventoryNo)}"));
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
            equipmentsViewSource = new CollectionViewSource { Source = Equipments };

            OnPropertyChanged(nameof(EquipmentsView));
            

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
