using System;
using System.Globalization;
using SpirAtheneum.Helpers;
using Xamarin.Forms;

namespace SpirAtheneum.Converters
{
    public class GreyOutCellConvertor: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Settings.IsSubscriped)
            {
                return "#7E313B";
            }
            else
            {
                return "#807E313B";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
