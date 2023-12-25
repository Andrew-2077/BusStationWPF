using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BusStationWPF.Model;

namespace BusStationWPF.Converters
{
    public class ConverterForPassengerGender : IValueConverter
    {
        public object Convert(object value, Type Target_Type, object Parameter, CultureInfo culture)
        {
            return ((PeopleGender)Parameter == (PeopleGender)value);
        }
        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;
        }
    }
}
