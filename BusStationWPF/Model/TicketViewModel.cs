using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class TicketViewModel
    {
        public PassengerViewModel Passenger { get; set; }
        public ConcreteWayBusModel Way { get; set; }
    }
}
