using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class RequestEditorViewModel : ViewModelBase
    {
        private string _Title = "Заявка";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }


    }
}
