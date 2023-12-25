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
using BusStationWPF.ViewModel.Interfaces;

namespace BusStationWPF.View
{
    /// <summary>
    /// Логика взаимодействия для EditStartTimePage.xaml
    /// </summary>
    public partial class EditStartTimePage : Page
    {
        public EditStartTimePage(IViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
