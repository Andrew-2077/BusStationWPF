using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel
{
    public class ChooseTicketService : IChooseTicketService
    {
        public event Action<List<Ticket>> ProcessComplete;
        public static event Action<List<WayModelForChooseTicket>> ShowNewWay;
        Dictionary<WayModelForChooseTicket, HashSet<CellStructureLevelModel>> ChoosenTickets = new Dictionary<WayModelForChooseTicket, HashSet<CellStructureLevelModel>>();
        public void SetConcreteWayFromStationToStation(List<WayModelForChooseTicket> way)
        {
            ChoosenTickets.Clear();
            way.ForEach(i => ChoosenTickets.Add(i, new HashSet<CellStructureLevelModel>()));
            ShowNewWay?.Invoke(way);
        }
        public void CancelCurrentProcessTicket()
        {
            ChoosenTickets.Clear();
        }
        public ICommand СompleteProcess
        {
            get => new RelayCommand((obj) =>
            {
                List<Ticket> Tickets = new List<Ticket>();
                ChoosenTickets.ToList().ForEach(i => i.Value.ToList().ForEach(cell => Tickets.Add(new Ticket()
                {
                    IdSeat = (int)cell.SeatId,
                    Cost = (i.Key.Way.EndStationBusScheduleModel.Number_flight - i.Key.Way.StartStationBusScheduleModel.Number_flight) * (int)cell.CostPerStation,
                    IdTimesForStationDestiny = i.Key.Way.EndTimesForStationModel.Id,
                    IdTimesForStationSource = i.Key.Way.StartTimesForStationModel.Id

                })));
                ProcessComplete?.Invoke(Tickets);
            }, (obj) =>
            {
                if (ChoosenTickets.Count == 0)
                    return false;
                int count = ChoosenTickets.First().Value.Count;
                if (count == 0)
                    return false;
                return ChoosenTickets.All(i => i.Value.Count == count);
            });
        }
        public ICommand DoProcess
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is object[] objects && objects[0] is CellStructureLevelModel cell && objects[1] is WayModelForChooseTicket wayModel)
                    ReserveOrFreeCell(cell, wayModel);
            });
        }
        void ReserveOrFreeCell(CellStructureLevelModel cell, WayModelForChooseTicket wayModel)
        {
            if (cell.typeOccupied == TypeOccupied.ReserveForBuy || cell.typeOccupied == TypeOccupied.Free)
            {
                cell.typeOccupied = cell.typeOccupied == TypeOccupied.ReserveForBuy ? TypeOccupied.Free : TypeOccupied.ReserveForBuy;
                if (cell.typeOccupied == TypeOccupied.ReserveForBuy)
                    ChoosenTickets[wayModel].Add(cell);
                else
                    ChoosenTickets[wayModel].Remove(cell);
            }
        }
    }
}
