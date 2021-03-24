using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Commands;
using EquipmentKP.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class RequestsViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА
        private readonly IRepository<Request> _RequestsRep;

        #region ObservableCollection<Request> Requests - Заявки
        private ObservableCollection<Request> _Requests;
        public ObservableCollection<Request> Requests
        {
            get => _Requests;
            set
            {
                if (!Set(ref _Requests, value)) return;

                _RequestsViewSource = new CollectionViewSource { Source = value };

                OnPropertyChanged(nameof(RequestsView));
                _RequestsViewSource.View.Refresh();

            }
        }
        #endregion

        #region View & ViewSource Requests - отображение заявкок
        private CollectionViewSource _RequestsViewSource;
        public ICollectionView RequestsView => _RequestsViewSource?.View;


        #endregion

        #region Request SelectedRequest - выбранная заявка
        private Request _SelectedRequest;

        public Request SelectedRequest
        {
            get => _SelectedRequest;
            set => Set(ref _SelectedRequest, value);
        }
        #endregion

        #region string Title - Заголовок окна
        private string _Title;
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion
        #endregion

        #region КОМАНДЫ

        #region LoadDataCommand - Команда загрузки данных из репозитория
        private ICommand _LoadDataCommand = null;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Requests = new ObservableCollection<Request>(await _RequestsRep.Items.ToArrayAsync());
        }
        #endregion

        #endregion


        public RequestsViewModel(IRepository<Request> RequestsRep)
        {
            _RequestsRep = RequestsRep;
        }

        public RequestsViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }

    }
}
