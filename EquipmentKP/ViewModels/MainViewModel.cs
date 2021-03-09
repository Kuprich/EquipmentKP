﻿using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.Services.Interfaces;
using EquipmentKP.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА

        private IRepository<MainEquipment> _EquipmentsRep;
        private readonly IUserDialog _UserDialog;

        #region String InventoryNo - поле для фильтра
        private String _InventoryNoFilter;

        public String InventoryNoFilter
        {
            get => _InventoryNoFilter;
            set
            {
                if (!Set(ref _InventoryNoFilter, value)) return;

                _EquipmentsViewSource.View.Refresh();
            }
        }
        #endregion

        #region View & ViewSource equipments (А так же фильтр)
        private CollectionViewSource _EquipmentsViewSource;
        public ICollectionView EquipmentsView => _EquipmentsViewSource?.View;
        private void EquipmentsViewSource_Filter(object sender, FilterEventArgs e)
        {
            if ( !(e.Item is MainEquipment equipment) || string.IsNullOrEmpty(InventoryNoFilter) ) return;

            if ( !equipment.EquipmentsKit.InventoryNo.Contains(InventoryNoFilter.Trim(), StringComparison.OrdinalIgnoreCase) )
                e.Accepted = false;
        } 
        #endregion

        #region string Title - заголовок окна
        private string _Title = "ИАЦ: Движение оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region ObservableCollection<MainEquipment> Equipments - оборудование
        private ObservableCollection<MainEquipment> _Equipments;
        public ObservableCollection<MainEquipment> Equipments
        {
            get => _Equipments;
            set
            {
                if (!Set(ref _Equipments, value)) return;

                _EquipmentsViewSource = new CollectionViewSource { Source = value };

                _EquipmentsViewSource.View.Refresh();
                OnPropertyChanged(nameof(EquipmentsView));
            }
        }
        #endregion

        #region MainEquipment SelectedEquipment - выбранное оборудование
        private MainEquipment _SelectedEquipment;

        public MainEquipment SelectedEquipment
        {
            get => _SelectedEquipment;
            set => Set(ref _SelectedEquipment, value);
        }
        #endregion

        #region Request SelectedRequest - выбранная заявка
        private Request _SelectedRequest;

        public Request SelectedRequest
        {
            get => _SelectedRequest;
            set => Set(ref _SelectedRequest, value);
        } 
        #endregion


        #endregion

        #region КОМАНДЫ

        #region CloseAplicationCommand - Команда закрытия окна
        private ICommand _CloseAplicationCommand = null;
        public ICommand CloseAplicationCommand => _CloseAplicationCommand ??= new LambdaCommand(OnCloseAplicationCommandExecuted);
        //private bool CanCloseAplicationCommandExecute() => true; // если этого параметра нет, то всегда разрешено выполнение данной команды
        private void OnCloseAplicationCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region LoadDataCommand - Команда загрузки данных из репозитория
        private ICommand _LoadDataCommand = null;
        public ICommand LoadDataCommand => _LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);
        private async Task OnLoadDataCommandExecuted()
        {
            Equipments = new ObservableCollection<MainEquipment>(await _EquipmentsRep.Items.ToArrayAsync());

            _EquipmentsViewSource.Filter += EquipmentsViewSource_Filter;
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription($"{nameof(EquipmentsKit)}.{nameof(EquipmentsKit.InventoryNo)}"));

        }
        #endregion

        #region GroupingCammand - Группировать (тестовая команда)
        private ICommand _GroupingCommand = null;
        public ICommand GroupingCammand => _GroupingCommand ?? new LambdaCommand(OnGroupingCammandExecute);
        private void OnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
            EquipmentsView.GroupDescriptions.Add(new PropertyGroupDescription($"{nameof(EquipmentsKit)}.{nameof(EquipmentsKit.InventoryNo)}"));
        }
        #endregion

        #region UnGroupingCammand - Разгруппировать
        private ICommand _UnGroupingCommand = null;
        public ICommand UnGroupingCammand => _UnGroupingCommand ?? new LambdaCommand(OnUnGroupingCammandExecute);
        private void OnUnGroupingCammandExecute()
        {
            EquipmentsView.GroupDescriptions.Clear();
        }
        #endregion

        #region EditEquipmentCommand - редактирование оборудования
        private ICommand _EditEquipmentCommand = null;
        public ICommand EditEquipmentCommand => _EditEquipmentCommand ?? new LambdaCommand(OnEditEquipmentCommandExecuted, CanEditEquipmentCommandExecute);
        public bool CanEditEquipmentCommandExecute(object p) => p is MainEquipment;
        public void OnEditEquipmentCommandExecuted(object p)
        {
            var equipment = (MainEquipment)p;
            if (_UserDialog.Edit(equipment))
            {
                // сохраннение информации в БД
            }
            else
            {
                // иниые действия
            }
        } 
        #endregion

        #endregion

        public MainViewModel(
            IRepository<MainEquipment> EquipmentsRep,
            IUserDialog UserDialog
            )
        {
            _EquipmentsRep = EquipmentsRep;
            _UserDialog = UserDialog;
            _EquipmentsViewSource = new CollectionViewSource { Source = Equipments };

            OnPropertyChanged(nameof(EquipmentsView));
            

            //var kit = new EquipmentsKit { InventoryNum = "000111000111", Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };

            //EquipmentsKitRep.Add(kit);
            //{ InventoryNum = "021384123", Location = locations[2], Owner = "УСД в Республике Мордовия", ReceiptDate = DateTime.Parse("30.08.2017") };
        }

        public MainViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }
    }
}