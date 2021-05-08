using Equipment.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace EquipmentKP.Infrastructure.Converters
{
    class EquipmentGroupItemsToCodeName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<object> items)
            {

                var item = (MainEquipment)items.First();

                return item?.EquipmentsKit?.Location?.CodeName;
            }

            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Обратное преобразование невозможно!");
        }
    }
}
