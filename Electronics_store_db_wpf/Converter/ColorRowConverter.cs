using System;
using System.Globalization;
using System.Windows.Data;

namespace Electronics_store_db_wpf.Converter
{
  
    public class ColorRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool property = (bool)value;

            return property ? "#52000D" : "Transparent";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string property = (string)value;

            return property == "#52000D";
        }
    }
}
