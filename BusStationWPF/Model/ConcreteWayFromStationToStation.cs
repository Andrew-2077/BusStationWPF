using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class ConcreteWayFromStationToStation
    {
        public List<ConcreteWayBusModel> ConcreteWayBusModels { get; set; }
        public string NameStartStation { get; set; }
        public string NameEndStation { get; set; }
        public DateTime DepartureTimeStartStation { get; set; }
        public DateTime ArrivingTimeEndStation { get; set; }

    }
}
