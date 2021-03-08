using Equipment.Database.Entities;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels;
using EquipmentKP.Views.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EquipmentKP.Services
{
    class UserDialogService : IUserDialog
    {
        public UserDialogService()
        {
            // при необоходимости передавать репозитории и сохранять их в приватные поля
        }
        public bool Edit(object item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            switch (item)
            {
                default: throw new NotSupportedException($"Редактирование объекта типа: {item.GetType()} не поддеживается");
                case MainEquipment equipment:
                    return EditEquipment(equipment);
            }
        }

        private bool EditEquipment(MainEquipment equipment)
        {
            var viewModel = new EquipmentEditorViewModel(equipment);
            var view = new EquipmentEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
                
            };

            return view.ShowDialog() ?? false;
        }
    }
}
