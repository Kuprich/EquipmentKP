using Equipment.Database.Entities;
using EquipmentKP.Infrastructure;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class EquipmentEditorViewModel : ViewModelBase
    {

        #region ПОЛЯ И СВОЙСТВА

        private readonly EquipmentsKit _EquipmentsKit;

        #region string Title - заголовок окна

        private string _Title = "Заголовок окна";
    
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        public string InventoryNo => _EquipmentsKit.InventoryNo;
        public string Owner => _EquipmentsKit.Owner?.Name;
        public DateTime ReceiptDate => _EquipmentsKit.ReceiptDate;
        public string Location => _EquipmentsKit.Location?.Name;

        public List<EquipmentType> EquipmentTypes { get; set; }

        #region  string Name - Наименование оборудования

        private string _Name;

        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion
        #region  string SerialNo - Серийный номер оборудования

        private string _SerialNo;

        public string SerialNo
        {
            get => _SerialNo;
            set => Set(ref _SerialNo, value);
        }

        #endregion
        #region  string IpAddress - IP адрес (если есть)

        private string _IpAddress;

        public string IpAddress
        {
            get => _IpAddress;
            set => Set(ref _IpAddress, value);
        }

        #endregion
        #region  string OperationSystem - Операционная система (если есть)

        private string _OperationSystem;

        public string OperationSystem
        {
            get => _OperationSystem;
            set => Set(ref _OperationSystem, value);
        }

        #endregion
        #region  string NetworkName - Сетевое имя оборудования

        private string _NetworkName;

        public string NetworkName
        {
            get => _NetworkName;
            set => Set(ref _NetworkName, value);
        }

        #endregion
        #region  EquipmentType SelectedEquipmentType - тип оборудования

        private EquipmentType _SelectedEquipmentType;

        public EquipmentType SelectedEquipmentType
        {
            get => _SelectedEquipmentType;
            set => Set(ref _SelectedEquipmentType, value);
        }

        #endregion

        #endregion

        public EquipmentEditorViewModel(EquipmentsKit EquipmentsKit)
        {
            _EquipmentsKit = EquipmentsKit;
        }
        public EquipmentEditorViewModel()
        {

        }

    }
}
