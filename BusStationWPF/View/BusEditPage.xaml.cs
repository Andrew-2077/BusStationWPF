using BusStationWPF.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusStationWPF.View
{
    /// <summary>
    /// Логика взаимодействия для BusEditPage.xaml
    /// </summary>
    public partial class BusEditPage : Page
    {
        public BusEditPage(IViewModel ViewModelObject)
        {
            InitializeComponent();
            var a = ViewModelObject.ShowerStructureLevel.StructureLevelWithoutSeats;
            rad.ItemsSource = a;
            this.DataContext = ViewModelObject;
        }
    }
}
