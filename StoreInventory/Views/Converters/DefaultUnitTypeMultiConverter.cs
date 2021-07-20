using System;
using System.Globalization;
using System.Windows.Data;

 namespace StoreInventory.Views.Converters
{
    public class DefaultUnitTypeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)values[0] == 0 && values[1].ToString() == String.Empty)
                return string.Empty;
            else
                return values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[])value;
        }
    }
}