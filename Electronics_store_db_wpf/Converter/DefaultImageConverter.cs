using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Electronics_store_db_wpf.Converter
{
    public class DefaultImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = value as string;

            if (string.IsNullOrEmpty(imagePath))
            {
                // Путь к дефолтному изображению 
                imagePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))), "Resources", "Img", "default_Img.jpg"); 
            }

            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
