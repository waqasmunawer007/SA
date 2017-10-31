using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpirAtheneum.Converters
{
    public class FavouriteImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var is_favourite = value.ToString();
                if(is_favourite == "false")
                {
                    return "icon_heart_empty.png";
                }
                else if(is_favourite == "true")
                {
                    return "icon_heart_filled.png";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
