using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace StoreInventory.Views.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                if ((int)value == 0)
                    return string.Empty;

            if (value is float)
                if ((float)value == 0f)
                    return string.Empty;

            return $"{value:C}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string digits;
            var price = value.ToString().Trim();
            if (price.Length > 1)
            {
                digits = price.Substring(1);  // Removing £ from the price
                return digits;
            }
            else
                return value;
        }
    }
}