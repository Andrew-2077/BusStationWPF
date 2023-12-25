using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model.StaticModelsForPassInfo
{
    public class InfoForPassToEditStationBusSchedulePage
    {
        public ObservableCollection<ModelForEditingSchedule> collection { get; set; }
        public List<StationModel> StationModels { get; set; }
    }
}
