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

        private readonly MainEquipment _Equipment;

        #region string Title - заголовок окна

        private string _Title = "Заголовок окна";
    
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        // один из вариантов
        //public string InventoryNo { get => _Equipment.EquipmentsKit.InventoryNo; set => _Equipment.EquipmentsKit.InventoryNo = value; }


        public string InventoryNo => _Equipment.EquipmentsKit.InventoryNo;
        public string Owner => _Equipment.EquipmentsKit.Owner.Name;
        public DateTime ReceiptDate => _Equipment.EquipmentsKit.ReceiptDate;
        public string Location => _Equipment.EquipmentsKit.Location.Name;
        


        #region  string SerialNo - Серийный номер оборудования
        private string _SerialNo;

        public string SerialNo
        {
            get => _SerialNo;
            set => Set(ref _SerialNo, value);
        } 
        #endregion



        #endregion

        public EquipmentEditorViewModel(MainEquipment Equipment)
        {
            _Equipment = Equipment;
        }

        public EquipmentEditorViewModel() : this(new MainEquipment())
        {
            if (!App.IsDesignTime)
                throw new NotSupportedException("Данный конструктор не предназначен для использования вне дизайнера visual Studio");
        }

    }
}
