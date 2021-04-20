using Equipment.Database.Entities;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.ViewModels
{
    class RequestMovementViewModel : ViewModelBase
    {
        public RequestMovement RequestMovement { get; }

        #region string Title - заголовок окна
        private string _Title = "Движение заявки";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region DateTime RegistrationDate - дата регистрации текущего состояния
        private DateTime _RegistrationDate;

        public DateTime RegistrationDate
        {
            get => _RegistrationDate;
            set => Set(ref _RegistrationDate, value);
        }
        #endregion

        #region List<RequestState> RequestStates - список возможных состояний
        private List<RequestState> _RequestStates;
        public List<RequestState> RequestStates
        {
            get => _RequestStates;
            set => Set(ref _RequestStates, value);
        }
        #endregion

        #region RequestState SelectedRequestState - выбранное состояние

        private RequestState _SelectedRequestState;

        public RequestState SelectedRequestState
        {
            get => _SelectedRequestState;
            set => Set(ref _SelectedRequestState, value);
        }
        

        #endregion

        public RequestMovementViewModel(RequestMovement RequestMovement)
        {
            this.RequestMovement = RequestMovement;
        }
        public RequestMovementViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }

    }
}
