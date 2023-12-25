using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IBusInProfileStrategy
    {
        void SetUser(UserModel user);
        List<BusInProfileModel> LoadBuses();
        void RemoveBus(ObservableCollection<BusInProfileModel> collection, BusInProfileModel bus);
        void EditBus(BusInProfileModel bus);
    }
}
