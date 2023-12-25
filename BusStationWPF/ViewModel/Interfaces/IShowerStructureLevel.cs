using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IShowerStructureLevel
    {
        ICommand ShowNextLevel { get; }
        ICommand ShowPreviousLevel { get; }
        void SetStructureLevelWithoutSeats(TypeOfLevelModel level);
        void SetStrucureWithSeats(List<WayModelForChooseTicket> way);
        ObservableCollection<List<CellStructureLevelModel>> StructureLevelWithoutSeats { get; }
        ObservableCollection<WayModelForChooseTicket> StructureLevelsWithSeats { get; }
    }
}
