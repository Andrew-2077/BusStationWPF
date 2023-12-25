using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusStationWPF.Model.ModelsForEditingViewStyle
{
    public class InfoButtonsOnBusEditPage : DependencyObject
    {
        public static readonly DependencyProperty IsEnableProperty =
        DependencyProperty.Register("IsEnable", typeof(bool),
        typeof(InfoButtonsOnBusEditPage));
        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set { SetValue(IsEnableProperty, value); }
        }
        public static InfoButtonsOnBusEditPage Instance { get; private set; }

        static InfoButtonsOnBusEditPage()
        {
            Instance = new InfoButtonsOnBusEditPage();
        }
    }
}
