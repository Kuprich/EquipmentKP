using Equipment.Database.Entities;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class EquipmentsKitEditorViewModel : ViewModelBase
    {
        #region СВОЙСТВА

        #region string Title - заголовко окна
        private string _Title = "Комплект оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        public EquipmentsKit _EquipmentsKit { get; }
        public IList<Owner> Owners { get; set; }
        public IList<Location> Locations { get; set; }
        public IList<MainEquipment> Equipments { get; set; }

        #region string InventoryNo - инвентарный номер оборудования
        private string _InventoryNo;

        public string InventoryNo
        {
            get => _InventoryNo;
            set => Set(ref _InventoryNo, value);
        }
        #endregion

        #region Owner SelectedOwner - выбранный владелец оборудования
        private Owner _SelectedOwner;
        public Owner SelectedOwner
        {
            get => _SelectedOwner;
            set => Set(ref _SelectedOwner, value);
        }
        #endregion

        #region Location SelectedLocation - выбранный владелец оборудования
        private Location _SelectedLocation;
        public Location SelectedLocation
        {
            get => _SelectedLocation;
            set => Set(ref _SelectedLocation, value);
        }
        #endregion

        #region DateTime ReceiptDate - дата получения оборудования
        private DateTime _ReceiptDate;
        public DateTime ReceiptDate
        {
            get => _ReceiptDate;
            set => Set(ref _ReceiptDate, value);
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

        #region EditEquipmentCommand - Редактирование оборудования
        private ICommand _EditEquipmentCommand = null;

        public ICommand EditEquipmentCommand => _EditEquipmentCommand ?? new LambdaCommand(OnEditEquipmentCommandExecuted, CanEditEquipmentCommandExecute);
        private bool CanEditEquipmentCommandExecute(object p) => p is MainEquipment;
        private void OnEditEquipmentCommandExecuted(object p)
        {
            var equipment = (MainEquipment)p;

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (_UserDialog.Edit(equipment))
            {
                // Equipments.Add(equipment);
                Equipments[Equipments.IndexOf(SelectedEquipment)] = equipment;
                OnPropertyChanged(nameof(Equipments));
                //_EquipmentsRep.Update(equipment);

                //_EquipmentsViewSource.View.Refresh();
            }
        }
        #endregion
    }
}
