using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model.ModelsForEditingViewStyle
{
    public class ButtonInfoBusEditPage
    {
        public bool IsEnabledButtonForAddLevel { get; set; }
        public bool IsEnabledButtonForAddStationInSchedule { get; set; }
        public bool IsEnabledButtonForSave { get; set; }
        public string ToolTipButtonForSave { get; set; }
    }
}
