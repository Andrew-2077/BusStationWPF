using BusStationWPF.Model.ModelsForGetInfoFromView;
using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface ISearchWayStrategy
    {
        List<ConcreteWayFromStationToStation> FindWays(InfoAboutSearchingWaysModel info);
    }
}
