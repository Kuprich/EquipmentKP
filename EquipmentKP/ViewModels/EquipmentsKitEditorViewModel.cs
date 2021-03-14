using Equipment.Database.Entities;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
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
