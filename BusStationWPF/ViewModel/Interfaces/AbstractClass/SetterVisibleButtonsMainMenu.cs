using BusStationWPF.Model.Enum;
using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces.AbstractClass
{
    public abstract class SetterVisibleButtonsMainMenu
    {
        public abstract void SetVisibleForButtons(MenuShow menuShow, TypeUser type);
    }
}
