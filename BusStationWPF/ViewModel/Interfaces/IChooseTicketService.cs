using BusStationWPF.Model;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IChooseTicketService
    {
        event Action<List<Ticket>> ProcessComplete;
        void SetConcreteWayFromStationToStation(List<WayModelForChooseTicket> way);
        void CancelCurrentProcessTicket();
        ICommand DoProcess { get; }
        ICommand СompleteProcess { get; }
    }
}
