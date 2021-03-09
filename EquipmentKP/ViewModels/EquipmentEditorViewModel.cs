using Equipment.Database.Entities;
using EquipmentKP.Infrastructure;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class EquipmentEditorViewModel : EditorViewModelBase
    {

        #region ПОЛЯ И СВОЙСТВА

        private readonly MainEquipment _Equipment;

        #region string InventoryNo - Комплект оборудования --> Серийный номер

        public string InventoryNo
        {
            get => GetValue(_Equipment.EquipmentsKit.InventoryNo);
            set => SetValue(value);
        } 
        #endregion

        #region string Title - заголовок окна

        private string _Title = "Заголовок окна";
    
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #endregion

        public EquipmentEditorViewModel(MainEquipment Equipment)
        {
            _Equipment = Equipment;
        }

        public void Commit()
        {
            var type = _Equipment.GetType();

            foreach (var (propertyName, value) in _Values)
            {
                var property = type.GetProperty(propertyName);
                if (property is null || property.CanWrite) continue;

                property.SetValue(_Equipment, value);
            }

            _Values.Clear();
        }
        public void Reject()
        {
            var properties = _Values.Keys.ToArray();
            _Values.Clear();

            foreach (var property in properties)
            {
                OnPropertyChanged(property);
            }
        }

        public EquipmentEditorViewModel() : this(new MainEquipment())
        {
            if (!App.IsDesignTime)
                throw new NotSupportedException("Данный конструктор не предназначен для использования вне дизайнера visual Studio");
        }

    }
}
