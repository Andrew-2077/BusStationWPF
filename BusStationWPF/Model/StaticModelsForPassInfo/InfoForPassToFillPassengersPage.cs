using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model.StaticModelsForPassInfo
{
    public static class InfoForPassToFillPassengersPage
    {
        public static ObservableCollection<PassengerViewModel> PassengersForTickets { get; } = new ObservableCollection<PassengerViewModel>();
        public static ObservableCollection<PassengerViewModel> PassengersInProfile { get; } = new ObservableCollection<PassengerViewModel>();
        public static ObservableCollection<StationModel> Stations { get; } = new ObservableCollection<StationModel>();
    }
}
