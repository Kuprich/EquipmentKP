using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EquipmentKP.Infrastructure.Converters
{
    class DocumentContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] content)
            {
                if (content != null && content?.Length > 0) return "Прикреплен документ";
            }

            return "Контент отсутствует";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Обратное преобразование невозможно!");
        }
    }
}
