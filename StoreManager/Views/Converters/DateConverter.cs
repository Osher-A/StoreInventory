using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Data;

namespace StoreManager.Views.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (DateTime)value == new DateTime(01, 01, 0001))
                return null;
            else
                return ((DateTime)value).ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateAsString = (string)value;
            DateTime date;
            bool isValidDate = DateTime.TryParse(dateAsString, culture, DateTimeStyles.None, out date);

            return date;
        }
    }
}