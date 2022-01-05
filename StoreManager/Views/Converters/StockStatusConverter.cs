using StoreManager.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace StoreManager.Views.Converters
{
    public class StockStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
                switch ((StockStatus)value)
                {
                    case StockStatus.WellStocked:
                        value = "Well stocked";
                        break;
                    case StockStatus.LowInStock:
                        value = "Low in stock";
                        break;
                    case StockStatus.OutOfStock:
                        value = "Out of stock";
                        break;
                }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}