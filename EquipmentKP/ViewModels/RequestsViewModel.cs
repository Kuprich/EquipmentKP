using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class RequestsViewModel : ViewModelBase
    {
        #region string Title - Заголовок окна
        private string _Title;
        public string Title { get => _Title; set => Set(ref _Title, value); } 
        #endregion

    }
}
