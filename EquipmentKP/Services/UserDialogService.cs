using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels;
using EquipmentKP.Views.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace EquipmentKP.Services
{
    class UserDialogService : IUserDialog
    {
        private readonly IEnumerable<Owner> _Owners;
        private readonly IEnumerable<Location> _Locations;
        private readonly IRepository<RequestState> _RequestStatesRep;

        public UserDialogService
            (
            IRepository<Owner> OwnersRep,
            IRepository<Location> LocationsRep,
            IRepository<RequestState> RequestStatesRep
            )
        {
            // при необоходимости передавать репозитории и сохранять их в приватные поля
            _Owners = OwnersRep.Items;
            _Locations = LocationsRep.Items;
            _RequestStatesRep = RequestStatesRep;
        }
        public bool Edit<T>(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            return item switch
            {
                MainEquipment   equipment       => EditEquipment(equipment),
                EquipmentsKit   equipmentsKit   => EditEquipmentsKit(equipmentsKit),
                Request         request         => EditRequest(request),
                RequestMovement requestMovement => EditRequestMovement(requestMovement),
                _ => throw new NotSupportedException($"Редактирование объекта типа: {item.GetType()} не поддеживается"),
            };
        }

        private bool EditRequestMovement(RequestMovement requestMovement)
        {
            var viewModel = new RequestStateEditorViewModel(requestMovement)
            {
                RegistrationDate = requestMovement.RegistrationDate == DateTime.MinValue ? DateTime.Now : requestMovement.RegistrationDate,
                RequestStates = new List<RequestState>(_RequestStatesRep.Items),
                SelectedRequestState = requestMovement.RequestState
            };
            var window = new RequestStateEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (window.ShowDialog() != true) return false;

            // присвоение данных
            requestMovement.RegistrationDate = viewModel.RegistrationDate;
            requestMovement.RequestState = viewModel.SelectedRequestState;

            return true;
        }
        private bool EditRequest(Request request)
        {
            var viewModel = new RequestEditorViewModel(request)
            {
                Title = "Окно просмотра и резактирования заявки",
                Number = request.Number,
                ReceiptDate = request.ReceiptDate == DateTime.MinValue ? DateTime.Now : request.ReceiptDate,
                Closed = request.Closed,
                Documents = new ObservableCollection<Document>(request.Documents),
                RequestMovements = new ObservableCollection<RequestMovement>(request.RequestMovements)
            };

            var window = new RequestEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            // присвоение данных
            request.Number = viewModel.Number;
            request.ReceiptDate = viewModel.ReceiptDate;
            request.Closed = viewModel.Closed;
            request.Documents = viewModel.Documents;
            request.RequestMovements = viewModel.RequestMovements;

            return true;
        }
        private bool EditEquipmentsKit(EquipmentsKit equipmentsKit)
        {
            var viewModel = new EquipmentsKitEditorViewModel(equipmentsKit)
            {
                Title = "Комплект оборудования",
                InventoryNo = equipmentsKit.InventoryNo,
                Owners = new List<Owner>(_Owners),
                Locations = new List<Location>(_Locations),
                SelectedLocation = equipmentsKit.Location,
                SelectedOwner = equipmentsKit.Owner,
                Equipments = new ObservableCollection<MainEquipment>(equipmentsKit.MainEquipments)
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
            equipmentsKit.ReceiptDate = viewModel.ReceiptDate;
            equipmentsKit.MainEquipments = viewModel.Equipments;

            return true;
        }
        private bool EditEquipment(MainEquipment equipment)
        {
            var viewModel = new EquipmentEditorViewModel(equipment.EquipmentsKit)
            {
                Title = "Редактирование оборудования",
                //InventoryNo = equipment.EquipmentsKit.InventoryNo,
                SerialNo = equipment.SerialNo
            };

            var window = new EquipmentEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            equipment.SerialNo = viewModel.SerialNo;

            return true;
            //return window.ShowDialog() ?? false;
        }

        public void ShowInformation(string Information, string Caption = "Информация") => MessageBox.Show(Information, Caption, MessageBoxButton.OK, MessageBoxImage.Information);
        public bool OpenFile(string filePath)
        {
            return false;    
        }
    }
}
