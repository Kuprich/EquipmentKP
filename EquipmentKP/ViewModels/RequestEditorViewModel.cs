using Equipment.Database.Entities;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        #region ObservableCollection RequestMovements - движение заявки
        private ObservableCollection<RequestMovement> _RequestMovements;
        public ObservableCollection<RequestMovement> RequestMovements
        {
            get => _RequestMovements;
            set => Set(ref _RequestMovements, value);
        }
        #endregion

        #region ObservableCollection Documents - Список документов, принадлежащих заявке
        private ObservableCollection<Document> _Documents;
        public ObservableCollection<Document> Documents
        {
            get => _Documents;
            set => Set(ref _Documents, value);
        }
        #endregion
        #endregion

        #region КОМАНДЫ

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
