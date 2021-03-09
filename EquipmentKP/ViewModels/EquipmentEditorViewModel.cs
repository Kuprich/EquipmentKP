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

        private readonly MainEquipment _Equipment;

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

        public EquipmentEditorViewModel() : this(new MainEquipment())
        {
            if (!App.IsDesignTime)
                throw new NotSupportedException("Данный конструктор не предназначен для использования вне дизайнера visual Studio");
        }

    }
}
