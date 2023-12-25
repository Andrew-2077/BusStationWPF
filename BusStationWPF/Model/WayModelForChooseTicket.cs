using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class WayModelForChooseTicket
    {
        public ObservableCollection<List<CellStructureLevelModel>> StructureLevelModels { get; set; } = new ObservableCollection<List<CellStructureLevelModel>>();
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public LevelModel LevelForShow { get; set; } = null;
        public ConcreteWayBusModel Way { get; set; }
    }
}
