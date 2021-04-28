using Equipment.Database.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace EquipmentKP.Infrastructure.Converters
{
    class LastRequestMovement : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<RequestMovement> collection)
                return collection.Last().RequestState.Name;
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Обратное преобразование невозможно!");
        }
    }
}
