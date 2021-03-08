using Equipment.Database.Entities;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class EquipmentEditorViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА

        private readonly MainEquipment equipment;

        #region string Title - заголовок окна

        private string title = "Заголовок окна";
    
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        #endregion

        #endregion

        public EquipmentEditorViewModel(MainEquipment Equipment)
        {
            equipment = Equipment;
        }

        public EquipmentEditorViewModel() : this(new MainEquipment())
        {
            if (!App.IsDesignTime)
                throw new NotSupportedException("Данный конструктор не предназначен для использования вне дизайнера visual Studio");
        }

    }
}
