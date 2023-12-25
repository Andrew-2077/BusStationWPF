using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class BusInProfileModel
    {
        public BusModel BusModel { get; set; }
        public List<string> Stations { get; set; }
    }
}
