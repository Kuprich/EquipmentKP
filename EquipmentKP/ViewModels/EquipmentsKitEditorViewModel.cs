using Equipment.Database.Entities;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class EquipmentsKitEditorViewModel : ViewModelBase
    {
        private readonly EquipmentsKit _EquipmentsKit;

        #region string Title - заголовко окна
        private string _Title = "Комплект оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion


        #region string InventoryNo - инвентарный номер оборудования
        private string _InventoryNo;

        public string InventoryNo
        {
            get => _InventoryNo;
            set => Set(ref _InventoryNo, value);
        }
        #endregion
        public IList<Owner> Owners { get; set; }
        #region Owner SelectedOwner - выбранный владелец оборудования
        private Owner _SelectedOwner;
        public Owner SelectedOwner
        {
            get => _SelectedOwner;
            set => Set(ref _SelectedOwner, value);
        }
        #endregion
        public IList<Location> Locations { get; set; }
        #region Owner SelectedLocation - выбранный владелец оборудования
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

        public EquipmentsKitEditorViewModel(EquipmentsKit EquipmentsKit)
        {
            _EquipmentsKit = EquipmentsKit;
        }

        public EquipmentsKitEditorViewModel()
        {
            if (!App.IsDesignTime) throw new InvalidOperationException("Даный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
    }
}
