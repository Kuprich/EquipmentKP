using Equipment.Database.Entities;
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

        #region string Title Заголовок окна

        private string _Title = "Заявка";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region string Number - Номер заявки

        private string _Number;
        public string Number
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
        #region RequestMovement SelectedRequestMovement - выбранное движение заявки

        private RequestMovement _SelectedRequestMovement;
        public RequestMovement SelectedRequestMovement
        {
            get => _SelectedRequestMovement;
            set => Set(ref _SelectedRequestMovement, value);
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
        #region Document SelectedDocument - выбранный документ

        private Document _SelectedDocument;
        public Document SelectedDocument
        {
            get => _SelectedDocument;
            set => Set(ref _SelectedDocument, value);
        }

        #endregion


        #endregion

        #region КОМАНДЫ

        #region AddRequestMovementCommand - добавление записи из таблицы "движение заявки"

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
        #region EditRequestMovementCommand - редактирование записи из таблицы "движение заявки"

        private ICommand _EditRequestMovementCommand = null;
        public ICommand EditRequestMovementCommand => _EditRequestMovementCommand ?? new LambdaCommand(OnEditRequestMovementCommandExecuted, CanEditRequestMovementCommandExecute);
        private bool CanEditRequestMovementCommandExecute(object p) => p != null;
        private void OnEditRequestMovementCommandExecuted(object p)
        {
            var requestMovement = (RequestMovement)p;

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (_UserDialog.Edit(requestMovement))
            {
                RequestMovements[RequestMovements.IndexOf(requestMovement)] = requestMovement;
                _RequestMovementsViewSource?.View.Refresh();
            }

        }

        #endregion
        #region DeleteRequestMovementCommand - удвление записи из таблицы "движение заявки"

        private ICommand _DeleteRequestMovementCommand = null;
        public ICommand DeleteRequestMovementCommand => _DeleteRequestMovementCommand ?? new LambdaCommand(OnDeleteRequestMovementCommandExecuted, CanDeleteRequestMovementCommandExecute);
        private bool CanDeleteRequestMovementCommandExecute(object p) => p != null;
        private void OnDeleteRequestMovementCommandExecuted(object p)
        {
            

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (!_UserDialog.Confirm("Вы собираетесь удалить одно из движений оборудования. Желаете продолжить?", "Внимание")) return;

            var requestMovement = (RequestMovement)p;

            RequestMovements.Remove(requestMovement);
            SelectedRequestMovement = null;

        }

        #endregion


        #region AddDocumentCommand - добавление документа

        private ICommand _AddDocumentCommand = null;
        public ICommand AddDocumentCommand => _AddDocumentCommand ?? new LambdaCommand(OnAddDocumentCommandExecuted);
        private void OnAddDocumentCommandExecuted()
        {
            var document = new Document
            {
                Request = Request
            };

            Documents.Add(document);

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (_UserDialog.Edit(document))
            {
                Documents[Documents.IndexOf(document)] = document;
                _DocumentsViewSource?.View.Refresh();
            }

            else Documents.Remove(document);
        }

        #endregion
        #region EditDocumentCommand - Редактирование документа

        private ICommand _EditDocumentCommand = null;
        public ICommand EditDocumentCommand => _EditDocumentCommand ?? new LambdaCommand(OnEditDocumentCommandExecuted, CanEditDocumentCommandExecute);
        private bool CanEditDocumentCommandExecute(object p) => p != null;
        private void OnEditDocumentCommandExecuted(object p)
        {
            var document = (Document)p;

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (_UserDialog.Edit(document))
            {
                Documents[Documents.IndexOf(document)] = document;
                _DocumentsViewSource?.View.Refresh();
            }

        }

        #endregion

        #region ShowUploadedDocumentCommand - Просмотр прикрепленного документа

        private ICommand _ShowUploadedDocumentCommand = null;
        public ICommand ShowUploadedDocumentCommand => _ShowUploadedDocumentCommand ?? new LambdaCommand(OnShowUploadedDocumentCommandExecuted, CanShowUploadedDocumentCommandExecute);
        private bool CanShowUploadedDocumentCommandExecute(object p) => p != null;
        private void OnShowUploadedDocumentCommandExecuted(object p)
        {
            var document = (Document)p;

            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            _UserDialog.ShowFile(document);

        }

        #endregion
        #region RemoveDocumentCommand - Удаление документа

        private ICommand _RemoveDocumentCommand = null;
        public ICommand RemoveDocumentCommand => _RemoveDocumentCommand ?? new LambdaCommand(OnRemoveDocumentCommandExecuted, CanRemoveDocumentCommandExecute);
        private bool CanRemoveDocumentCommandExecute(object p) => p != null;
        private void OnRemoveDocumentCommandExecuted(object p)
        {
            using var scope = App.Host.Services.CreateScope();
            var _UserDialog = scope.ServiceProvider.GetRequiredService<IUserDialog>();

            if (!_UserDialog.Confirm("Вы собираетесь удалить один из документов. Желаете продолжить?", "Внимание")) return;

            var document = (Document)p;

            Documents.Remove(document);
            SelectedDocument = null;

        }

        #endregion

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
