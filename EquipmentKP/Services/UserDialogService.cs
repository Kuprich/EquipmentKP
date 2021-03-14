using Equipment.Database.Entities;
using Equipment.Interfaces;
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
        private readonly IEnumerable<Owner> _Owners;
        private readonly IEnumerable<Location> _Locations;

        public UserDialogService
            (
            IRepository<Owner> OwnersRep,
            IRepository<Location> LocationsRep
            )
        {
            // при необоходимости передавать репозитории и сохранять их в приватные поля
            _Owners = OwnersRep.Items;
            _Locations = LocationsRep.Items;
        }

        public bool Add<T>(T item)
        {
            return item switch
            {
                EquipmentsKit equipmentsKit => AddEquipmentsKit(equipmentsKit),
                _ => throw new NotSupportedException($"Добавление объекта типа: {item.GetType()} не поддеживается"),
            };
        }
        private bool AddEquipmentsKit(EquipmentsKit equipmentsKit)
        {

            var viewModel = new EquipmentsKitEditorViewModel(equipmentsKit)
            {
                Title = "Добавление комплекта оборудования",
                Owners = new List<Owner>(_Owners),
                Locations = new List<Location>(_Locations)
            };

            var window = new EquipmentsKitEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            // присвоение данных
            equipmentsKit.InventoryNo = viewModel.InventoryNo;
            equipmentsKit.Location = viewModel.SelectedLocation;
            equipmentsKit.Owner = viewModel.SelectedOwner;

            return true;
        }
        public bool Edit<T>(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            return item switch
            {
                MainEquipment equipment     => EditEquipment(equipment),
                EquipmentsKit equipmentsKit => EditEquipmentsKit(equipmentsKit),
                _ => throw new NotSupportedException($"Редактирование объекта типа: {item.GetType()} не поддеживается"),
            };
        }

        private bool EditEquipmentsKit(EquipmentsKit equipmentsKit)
        {
            var viewModel = new EquipmentsKitEditorViewModel(equipmentsKit)
            {
                Title = "Редактирование комплекта"
            };

            var window = new EquipmentEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            return true;
        }

        private bool EditEquipment(MainEquipment equipment)
        {
            var viewModel = new EquipmentEditorViewModel(equipment)
            {
                Title = "Редактирование оборудования",
                InventoryNo = equipment.EquipmentsKit.InventoryNo
            };

            var window = new EquipmentEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            equipment.EquipmentsKit.InventoryNo = viewModel.InventoryNo;

            return true;
            //return window.ShowDialog() ?? false;
        }
        public void ShowInformation(string Information, string Caption = "Информация") => MessageBox.Show(Information, Caption, MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
