﻿using Equipment.Database.Entities;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class RequestEditorViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА
        public Request Request { get; }

        #region Заголовок окна

        private string _Title = "Заявка";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region int Number - Номер заявки

        private int _Number;
        public int Number
        {
            get => _Number;
            set => Set(ref _Number, value);
        }

        #endregion

        #region DateTime ReceiptDate - Дата регистрации заявки

        private DateTime _ReceiptDate;
        public DateTime ReceiptDate
        {
            get => _ReceiptDate;
            set => Set(ref _ReceiptDate, value);
        }

        #endregion

        #region bool Closed - Отметка о выполнении

        private bool _Closed;
        public bool Closed
        {
            get => _Closed;
            set => Set(ref _Closed, value);
        }

        #endregion

        #region View & ViewSource RequestMovements
        private CollectionViewSource _RequestMovementsViewSource;
        public ICollectionView RequestMovementsView => _RequestMovementsViewSource?.View;
        #endregion
        #region ObservableCollection RequestMovements - движение заявки

        private ObservableCollection<RequestMovement> _RequestMovements;
        public ObservableCollection<RequestMovement> RequestMovements
        {
            get => _RequestMovements;
            set
            {
                if (!Set(ref _RequestMovements, value)) return;

                _RequestMovementsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(RequestMovementsView));
                _RequestMovementsViewSource.View.Refresh();
            }

        }

        #endregion

        #region View & ViewSource Documents

        private CollectionViewSource _DocumentsViewSource;
        public ICollectionView DocumentsView => _DocumentsViewSource?.View;

        #endregion
        #region ObservableCollection Documents - Список документов, принадлежащих заявке

        private ObservableCollection<Document> _Documents;
        public ObservableCollection<Document> Documents
        {
            get => _Documents;
            set
            {
                if (!Set(ref _Documents, value)) return;

                _DocumentsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(DocumentsView));
                _DocumentsViewSource.View.Refresh();

            }
        }

        #endregion

        #endregion

        #region КОМАНДЫ

        private ICommand _AddRequestMovementCommand = null;
        public ICommand AddRequestMovementCommand => _AddRequestMovementCommand ?? new LambdaCommand(OnAddRequestMovementCommandExecuted);
        private void OnAddRequestMovementCommandExecuted()
        {
            var requestMovement = new RequestMovement
            {
                Request = Request
            };

            RequestMovements.Add(requestMovement);

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (_UserDialog.Edit(requestMovement))
            {
                RequestMovements[RequestMovements.IndexOf(requestMovement)] = requestMovement;
                _RequestMovementsViewSource?.View.Refresh();
            }

            else RequestMovements.Remove(requestMovement);
        }

        #endregion

        public RequestEditorViewModel(Request Request)
        {
            this.Request = Request;
        }
        public RequestEditorViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");

        }

    }
}