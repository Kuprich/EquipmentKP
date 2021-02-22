using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace EquipmentKP.ViewModels.Base
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //пример использования: Set(ref field, value);
        protected virtual bool Set<T>(ref T Field, T Value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(Field, Value)) return false;
            Field = Value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        //если значение нельзя передаль по ссылке, то используется данный оператор: 
        // пример изпользования: Set(value, myModel.A, v => _QuadraticEquation.A = v)
        protected virtual bool Set<T>(T Value, T OldValue, Action<T> Setter, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(OldValue, Value)) return false;
            Setter(Value);
            OnPropertyChanged(PropertyName);
            return true;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged
            ?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
