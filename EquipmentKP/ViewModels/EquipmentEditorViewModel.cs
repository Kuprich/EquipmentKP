using Equipment.Database.Entities;
using EquipmentKP.Infrastructure;
using EquipmentKP.Infrastructure.Command;
using EquipmentKP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace EquipmentKP.ViewModels
{
    class EquipmentEditorViewModel : ViewModelBase
    {
        public event EventHandler<EventArgs<bool>> Complete;

        private readonly Dictionary<string, object> _Values = new Dictionary<string, object>();
        protected virtual bool SetValue(object value, [CallerMemberName] string Property = null)
        {
            if (_Values.TryGetValue(Property, out var old_value) && Equals(value, old_value))
                return false;
            _Values[Property] = value;
            OnPropertyChanged(Property);
            return true;
        }
        protected virtual T GetValue<T>(T Default, [CallerMemberName] string Property = null)
        {
            if (_Values.TryGetValue(Property, out var value))
                return (T)value;
            return Default;
        }


        #region ПОЛЯ И СВОЙСТВА

        private readonly MainEquipment _Equipment;

        #region string InventoryNo - Комплект оборудования --> Серийный номер

        public string InventoryNo
        {
            get => GetValue(_Equipment.EquipmentsKit.InventoryNo);
            set => SetValue(value);
        } 
        #endregion

        #region string Title - заголовок окна

        private string _Title = "Заголовок окна";
    
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #endregion

        #region КОМАНДЫ

        #region CancelCommand - Отмена изменений
        private ICommand _CancelCommand = null;
        public ICommand CancelCommand => _CancelCommand ??
            new LambdaCommand(OnCancelCommandExecuted);
        private void OnCancelCommandExecuted()
        {
            Reject();
            Complete?.Invoke(this, false);
        }
        #endregion

        #region RejectCommand - Сброс изменений
        private ICommand _RejectCommand = null;
        public ICommand RejectCommand => _RejectCommand ??
            new LambdaCommand(OnRejectCommandExecuted, CanRejectCommandExecute);
        private bool CanRejectCommandExecute(object p) => _Values.Count > 0;
        private void OnRejectCommandExecuted(object p)
        {
            Reject();
        }
        #endregion

        #region CommitCommand - Примение изменений
        private ICommand _CommitCommand = null;
        public ICommand CommitCommand => _CommitCommand ??
            new LambdaCommand(OnCommitCommandExecuted, CanCommitCommandExecute);
        private bool CanCommitCommandExecute(object p) => true;
        private void OnCommitCommandExecuted(object p)
        {
            Commit();
            Complete?.Invoke(this, false);
        } 
        #endregion

        #endregion

        public EquipmentEditorViewModel(MainEquipment Equipment)
        {
            _Equipment = Equipment;
        }

        public void Commit()
        {
            var type = _Equipment.GetType();

            foreach (var (propertyName, value) in _Values)
            {
                var property = type.GetProperty(propertyName);
                if (property is null || property.CanWrite) continue;

                property.SetValue(_Equipment, value);
            }

            _Values.Clear();
        }
        public void Reject()
        {
            var properties = _Values.Keys.ToArray();
            _Values.Clear();

            foreach (var property in properties)
            {
                OnPropertyChanged(property);
            }
        }

        public EquipmentEditorViewModel() : this(new MainEquipment())
        {
            if (!App.IsDesignTime)
                throw new NotSupportedException("Данный конструктор не предназначен для использования вне дизайнера visual Studio");
        }

    }
}
