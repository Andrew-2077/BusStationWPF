using BusStationWPF.Model;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IGetCollectionsService
    {
        void SetUser(UserModel user);
        List<Station> Stations { get; }
        List<PassengerViewModel> PassengerInProfile { get; }
        List<StationModel> StationModels { get; }
    }
}
