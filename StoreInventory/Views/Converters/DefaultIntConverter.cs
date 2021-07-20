using StoreInventory.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace StoreInventory.Views.Converters
{
    public class DefaultIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                if ((int)value == 0)
                    return string.Empty;

            if (value is float)
                if ((float)value == 0f)
                    return string.Empty;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

