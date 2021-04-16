using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.Services.Interfaces
{
    interface IUserDialog
    {
        bool Edit<T>(T item);
        void ShowInformation(string Information, string Caption = "Информация");
        bool OpenFile(string filePath);
    }
}
