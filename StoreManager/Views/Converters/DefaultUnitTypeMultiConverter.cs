using StoreManager.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

 namespace StoreManager.Views.Converters
{
    public class DefaultUnitTypeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //If no item is selected it shouldn't show up the default value of 0
            if (values[0] == (object)"Single" && values[1].ToString() == String.Empty)
                return string.Empty;
            else
                return values[0].ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var objArray = new object[1];
            objArray[0] = (int)value;

            return objArray;
        }
    }
}