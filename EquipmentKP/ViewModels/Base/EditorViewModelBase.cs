using EquipmentKP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace EquipmentKP.ViewModels.Base
{
    class EditorViewModelBase : ViewModelBase
    {
        public event EventHandler<EventArgs<bool>> Complete;

        protected readonly Dictionary<string, object> _Values = new Dictionary<string, object>();
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
        
        

    }
}
