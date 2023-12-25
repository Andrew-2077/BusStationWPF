﻿using BusStationWPF.Model.Collections;
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
    /// Логика взаимодействия для SearchWaysForReportPage.xaml
    /// </summary>
    public partial class SearchWaysForReportPage : Page
    {
        IViewModel model;
        public SearchWaysForReportPage(IViewModel model1)
        {
            InitializeComponent();
            this.model = model1;
            ((StationModelCollection)TryFindResource("Stations")).Collection = model1.GetCollectionsService.StationModels;
            ((ConcreteWayFromStationToStationObservableCollection)TryFindResource("Waysfound")).Collection = model1.SearcherWaysService.PathsFound;
            DataContext = model1;
        }
    }
}
