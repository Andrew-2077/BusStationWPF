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
    public class ConverterForViewSeat : IMultiValueConverter
    {
        public object Convert(object[] Values, Type Target_Type, object Parameter, CultureInfo culture)
        {
            return new PasswordChangeModel(Values[0] as BehaviourPasswordBox, Values[1] as BehaviourPasswordBox);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}