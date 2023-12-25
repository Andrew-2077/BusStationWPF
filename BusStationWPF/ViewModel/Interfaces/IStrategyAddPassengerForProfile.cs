using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IStrategyAddPassengerForProfile
    {
        void SetUser(UserModel user);
        void AddPassenger(ObservableCollection<PassengerViewModel> passengers);
        void Remove(ObservableCollection<PassengerViewModel> passengers, PassengerViewModel passenger);
        bool Validate(PassengerViewModel passenger);
        void Save(PassengerViewModel passenger);
        List<PassengerViewModel> LoadPassengers();
    }
}
