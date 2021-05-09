using Equipment.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.Services.Interfaces
{
    interface IUserDialog
    {
        bool Edit<T>(T item);
        void ShowInformation(string Information = "Текст сообщения", string Caption = "Информация");
        public bool Confirm(string Message = "Текст сообщения", string Caption = "Информация", bool Exclamation = false);
        bool UploadFile(string filePath);
        void ShowFile(Document document);
    }
}
