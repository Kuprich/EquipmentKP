using Equipment.Database.Entities;
using Equipment.Interfaces;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
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
    class MainWindowViewModel : ViewModelBase
    {
        #region ПОЛЯ И СВОЙСТВА

        private readonly IRepository<Location> localtionsRep;
        private readonly IRepository<SubEquipmentType> subEquipmentTypesRep;
        private readonly IRepository<MainEquipmentType> mainEquipmentTypesRep;
        private readonly IRepository<MainEquipment> mainEquipmentsRep;
        private readonly IRepository<SubEquipment> subEquipmentsRep;

        #region string Title - заголовок окна
        private string _Title = "ИАЦ: Движение оборудования";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region  List<string> MainEquipmentType список типов основного оборудования
        public List<string> MainEquipmentType
        {
            get
            {
                return mainEquipmentTypesRep.Items.Select(s => s.Name).ToList();
            }
        }
        #endregion

        #region  List<string> Location список мест установки оборудования
        public List<string> Location
        {
            get
            {
                return localtionsRep.Items.Select(s => s.Name).ToList();
            }
        }
        #endregion

        #region  MainEquipment SelectedMainEquipment - выбранное основное оборудование
        private MainEquipment selectedMainEquipment;

        public MainEquipment SelectedMainEquipment
        {
            get => selectedMainEquipment;
            set => Set(ref selectedMainEquipment, value);
        } 
        #endregion


        #region V и VS (collectionVSMainEquipment / collectionVMainEquipment) - отображение данных из главного репозитория
        private readonly CollectionViewSource collectionVSMainEquipment;
        public ICollectionView CollectionVMainEquipment => collectionVSMainEquipment?.View;
        #endregion

        #region int EquipmentsCount - количество данных в основной таблице с оборудованием
        public int EquipmentsCount
        {
            get => CollectionVMainEquipment.Count();
        } 
        #endregion


        #endregion

        #region КОМАНДЫ
        #region CloseAplicationCommand - Команда закрытия окна
        private ICommand _CloseAplicationCommand;
        public ICommand CloseAplicationCommand => _CloseAplicationCommand ??= new LambdaCommand(OnCloseAplicationCommandExecuted);
        //private bool CanCloseAplicationCommandExecute() => true; // если этого параметра нет, то всегда разрешено выполнение данной команды
        private void OnCloseAplicationCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region TestAsyncCommand - пример асинхронной команды
        private ICommand _TestAsyncCommand;
        public ICommand TestAsyncCommand => _TestAsyncCommand ??= new LambdaCommandAsync(OnTestAsyncCommandExecuted);
        private DateTime _Time;

        public DateTime Time
        {
            get => _Time;
            set => Set(ref _Time, value);
        }

        private async Task OnTestAsyncCommandExecuted(object p) => await Task.Run(TestAsyncMethod);

        private void TestAsyncMethod()
        {
            Thread.Sleep(5000);
            Time = DateTime.Now;
        }
        #endregion 
        #endregion

        public MainWindowViewModel(
            IRepository<Location> localtionsRep,
            IRepository<SubEquipmentType> subEquipmentTypesRep,
            IRepository<MainEquipmentType> mainEquipmentTypesRep,
            IRepository<MainEquipment> mainEquipmentsRep,
            IRepository<SubEquipment> subEquipmentsRep
            )
        {
            this.localtionsRep = localtionsRep;
            this.subEquipmentTypesRep = subEquipmentTypesRep;
            this.mainEquipmentTypesRep = mainEquipmentTypesRep;
            this.mainEquipmentsRep = mainEquipmentsRep;
            this.subEquipmentsRep = subEquipmentsRep;

            collectionVSMainEquipment = new CollectionViewSource { Source = mainEquipmentsRep.Items.ToArray() };
        }
    }
}
