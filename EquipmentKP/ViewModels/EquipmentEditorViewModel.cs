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
        
        #region  string SerialNo - Серийный номер оборудования
        private string _SerialNo;

        public string SerialNo
        {
            get => _SerialNo;
            set => Set(ref _SerialNo, value);
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
