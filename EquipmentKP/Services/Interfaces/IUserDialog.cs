using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.Services.Interfaces
{
    interface IUserDialog
    {
        bool Edit(object item);
        bool Add(object item);
        void ShowInformation(string Information, string Caption = "Информация");

    }
}
