using StoreInventory.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace StoreInventory.Views.Converters
{
    public class PaymentStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PaymentStatus?)value == PaymentStatus.FullyPaid)
                if (parameter.ToString() == "Fully paid")
                    return true;
                else
                    return false;
            else if ((PaymentStatus?)value == PaymentStatus.PartlyPaid)
                if (parameter.ToString() == "Partly paid")
                    return true;
                else
                    return false;
            else if ((PaymentStatus?)value == PaymentStatus.NotPaid)
                if (parameter.ToString() == "Not paid")
                    return true;
                else
                    return false;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                PaymentStatus? ps = null;
                switch (parameter)
                {
                    case "Fully paid":
                        ps = PaymentStatus.FullyPaid;
                        break;
                    case "Partly paid":
                        ps = PaymentStatus.PartlyPaid;
                        break;
                    case "Not paid":
                        ps = PaymentStatus.NotPaid;
                        break;
                }
                return ps;
            }
            else
                return null;
        }
    }
}
