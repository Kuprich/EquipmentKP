﻿using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels;
using EquipmentKP.Views.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace EquipmentKP.Services
{
    class UserDialogService : IUserDialog
    {
        private readonly IEnumerable<Owner> _Owners;
        private readonly IEnumerable<Location> _Locations;
        private readonly IRepository<RequestState> _RequestStatesRep;
        private readonly IRepository<EquipmentType> _EquipmentTypesRep;

        public UserDialogService
            (
            IRepository<Owner> OwnersRep,
            IRepository<Location> LocationsRep,
            IRepository<RequestState> RequestStatesRep,
            IRepository<EquipmentType> EquipmentTypesRep
            )
        {
            // при необоходимости передавать репозитории и сохранять их в приватные поля
            _Owners = OwnersRep.Items;
            _Locations = LocationsRep.Items;
            _RequestStatesRep = RequestStatesRep;
            _EquipmentTypesRep = EquipmentTypesRep;
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
                Document        document        => EditDocument(document),
                _ => throw new NotSupportedException($"Редактирование объекта типа: {item.GetType()} не поддеживается"),
            };
        }



        #region Команды редактирования объектов

        private bool EditDocument(Document document)
        {
            var viewModel = new DocumentEditorViewModel(document)
            {
                Content = document.Content,
                CreationDate = document.CreationDate == DateTime.MinValue ? DateTime.Now : document.CreationDate,
                Name = document.Name,
                Number = document.Number,
                FileType = document.FileType
            };
            var window = new DocumentEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            // присвоение данных
            document.Name = viewModel.Name;
            document.Number = viewModel.Number;
            document.CreationDate = viewModel.CreationDate;
            document.Content = viewModel.Content;
            document.FileType = viewModel.FileType;

            return true;
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
                IpAddress = equipment.IpAddress,
                NetworkName = equipment.NetworkName,
                OperationSystem = equipment.OperationSystem,
                SerialNo = equipment.SerialNo,
                SelectedEquipmentType = equipment.EquipmentType,
                EquipmentTypes = new List<EquipmentType>(_EquipmentTypesRep.Items)
            };

            var window = new EquipmentEditorWindow
            {
                DataContext = viewModel,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() != true) return false;

            // присвоение данных
            equipment.IpAddress = viewModel.IpAddress;
            equipment.NetworkName = viewModel.NetworkName;
            equipment.OperationSystem = viewModel.OperationSystem;
            equipment.SerialNo = viewModel.SerialNo;
            equipment.EquipmentType = viewModel.SelectedEquipmentType;

            return true;
        }

        #endregion

        #region Показать / прикрепить файл
        public bool UploadFile(string filePath)
        {
            return false;
        }
        public void ShowFile(Document document)
        {
            if (document.Content is null && document.Content?.Length <= 0) return;

            string fileName = $"tmp{document.FileType}";
            string dirPath = Environment.CurrentDirectory;
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
                dirInfo.Create();
            File.WriteAllBytes(dirPath + fileName, document.Content);

            new Process { StartInfo = new ProcessStartInfo(dirPath + fileName) { UseShellExecute = true } }.Start();
        }
        #endregion

        public void ShowInformation(string Information = "Текст сообщения", string Caption = "Информация") => MessageBox.Show(Information, Caption, MessageBoxButton.OK, MessageBoxImage.Information);

        public bool Confirm(string Message = "Текст сообщения" , string Caption = "Информация", bool Exclamation = false) =>
            MessageBox.Show
            (
                Message,
                Caption,
                MessageBoxButton.YesNo,
                Exclamation ? MessageBoxImage.Exclamation : MessageBoxImage.Question
            ) 
            == MessageBoxResult.Yes;

    }
}
