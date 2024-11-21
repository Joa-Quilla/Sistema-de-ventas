using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CELLTECH_COM.Converters
{
    public class MessageColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string message)
            {
                if (message.StartsWith("✓"))
                    return new SolidColorBrush(Colors.Green);
                else if (message.StartsWith("✗"))
                    return new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}