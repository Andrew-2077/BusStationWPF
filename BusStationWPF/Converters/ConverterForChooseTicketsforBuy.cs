﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BusStationWPF.Converters
{
    public class ConverterForChooseTicketsforBuy : IMultiValueConverter
    {
        public object Convert(object[] Values, Type Target_Type, object Parameter, CultureInfo culture)
        {
            return Values.Clone();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
