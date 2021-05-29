using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class EquipmentsKitEditorViewModel : ViewModelBase
    {

        #region ПОЛЯ И СВОЙСТВА

        private IUserDialog _UserDialog;

        #region string Title - заголовко окна
        private string _Title = "Комплект оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        public EquipmentsKit EquipmentsKit { get; }
        public IList<Owner> Owners { get; set; }
        public IList<Location> Locations { get; set; }

        #region ObservableCollection<MainEquipment> Equipments - оборудование, входящее в состав комплекта
        private ObservableCollection<MainEquipment> _Equipments;
        public ObservableCollection<MainEquipment> Equipments
        {
            get => _Equipments;
            set
            {
                if (!Set(ref _Equipments, value)) return;

                _EquipmentsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(EquipmentsView));
                _EquipmentsViewSource.View.Refresh();
            }
        } 
        #endregion

        #region View & ViewSource equipments
        private CollectionViewSource _EquipmentsViewSource;
        public ICollectionView EquipmentsView => _EquipmentsViewSource?.View;
        #endregion

        #region string InventoryNo - инвентарный номер оборудования
        private string _InventoryNo;

        public string InventoryNo
        {
            get => _InventoryNo;
            set
            {
                if (!Set(ref _InventoryNo, value)) return;
                EquipmentsKit.InventoryNo = value;
            }
                
        }
        #endregion

        #region Owner SelectedOwner - выбранный владелец оборудования
        private Owner _SelectedOwner;
        public Owner SelectedOwner
        {
            get => _SelectedOwner;
            set
            {
                if (!Set(ref _SelectedOwner, value)) return;
                EquipmentsKit.Owner = value;
            }
        }
        #endregion

        #region Location SelectedLocation - выбранный владелец оборудования
        private Location _SelectedLocation;
        public Location SelectedLocation
        {
            get => _SelectedLocation;
            set
            {
                if (!Set(ref _SelectedLocation, value)) return;
                EquipmentsKit.Location = value;
            }
        }
        #endregion

        #region DateTime ReceiptDate - дата получения оборудования
        private DateTime _ReceiptDate;
        public DateTime ReceiptDate
        {
            get => _ReceiptDate;
            set
            {
                if (!Set(ref _ReceiptDate, value)) return;
                EquipmentsKit.ReceiptDate = value;
            }
        }
        #endregion

        #region MainEquipment SelectedEquipment - выбранное оборудование
        private MainEquipment _SelectedEquipment;
        public MainEquipment SelectedEquipment
        {
            get => _SelectedEquipment;
            set => Set(ref _SelectedEquipment, value);
        }
        #endregion
        #endregion

        #region КОМАНДЫ

        #region AddEquipmentCommand - Добавление оборудования

        private ICommand _AddEquipmentCommand = null;
        public ICommand AddEquipmentCommand => _AddEquipmentCommand ?? new LambdaCommand(OnAddEquipmentCommandExecuted);
        private void OnAddEquipmentCommandExecuted()
        {
            var equipment = new MainEquipment
            {
                EquipmentsKit = EquipmentsKit
            };
            Equipments.Add(equipment);

            if (_UserDialog.Edit(equipment))
            {
                Equipments[Equipments.IndexOf(equipment)] = equipment;
                _EquipmentsViewSource.View.Refresh();
            }
            else
                Equipments.Remove(equipment);
        }

        #endregion 
        #region EditEquipmentCommand - Редактирование оборудования
        private ICommand _EditEquipmentCommand = null;
        public ICommand EditEquipmentCommand => _EditEquipmentCommand ?? new LambdaCommand(OnEditEquipmentCommandExecuted, CanEditEquipmentCommandExecute);
        private bool CanEditEquipmentCommandExecute(object p) => p is MainEquipment;
        private void OnEditEquipmentCommandExecuted(object p)
        {
            var equipment = (MainEquipment)p;

            if (_UserDialog.Edit(equipment))
            {
                Equipments[Equipments.IndexOf(SelectedEquipment)] = equipment;
                _EquipmentsViewSource.View.Refresh();
            }
        }
        #endregion
        #region RemoveEquipmentCommand - Редактирование оборудования

        private ICommand _RemoveEquipmentCommand = null;
        public ICommand RemoveEquipmentCommand => _RemoveEquipmentCommand ?? new LambdaCommand(OnRemoveEquipmentCommandExecuted, CanRemoveEquipmentCommandExecute);
        private bool CanRemoveEquipmentCommandExecute(object p) => p is MainEquipment;
        private void OnRemoveEquipmentCommandExecuted(object p)
        {
            var equipment = (MainEquipment)p;

            if (!_UserDialog.Confirm($"Вы собираетесь удалить оборудование \"{equipment.Name}\" а так же связанную с ним информацию. Вернуть данные будет невозможно! Желаете продолжить?", "Внимание")) return;

            Equipments.Remove(equipment);
            SelectedEquipment = null;
        }

        #endregion

        #endregion

        public EquipmentsKitEditorViewModel(EquipmentsKit EquipmentsKit)
        {
            this.EquipmentsKit = EquipmentsKit;

            using var scope = App.Host.Services.CreateScope();
            _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();
        }

        public EquipmentsKitEditorViewModel()
        {
        //TODO: Возможно сделать проверку на isDesignTime  
        }

    }
}
