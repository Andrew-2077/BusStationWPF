using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.StrategyForProfile
{
    public class SimpleTicketControlStrategy : ITicketContolStrategyInProfile
    {
        UserModel user;
        IUnitOfWork db;
        public SimpleTicketControlStrategy(IUnitOfWork db)
        {
            this.db = db;
        }

        public void SetUser(UserModel user)
        {
            this.user = user;
        }
        public List<TicketModelForProfile> LoadTickets()
        {
            return (from tickets in db.Ticket.GetList()
                    where tickets.UserId == user.Id
                    select tickets).ToList().Select(i => LoadTicket(i)).ToList();
        }
        private TicketModelForProfile LoadTicket(Ticket ticket)
        {
            PassengerViewModel passengerForTicket = new PassengerViewModel(db.Passengers.GetItem(ticket.IdPassenger), true);
            TimesForStation startTimesForStation = db.TimesForStation.GetItem(ticket.IdTimesForStationSource);
            TimesForStation endTimesForStation = db.TimesForStation.GetItem(ticket.IdTimesForStationDestiny);
            ScheduleStationBus startStationBusSchedule = db.ScheduleStationBus.GetList().Where(i => i.Number_flight ==
            db.TimesForStation.GetList().Where(j => j.RouteId == startTimesForStation.RouteId && j.DepartureTime < startTimesForStation.ArrivalTime).Count() + 1).First();
            ScheduleStationBus endStationBusSchedule = db.ScheduleStationBus.GetList().Where(i => i.Number_flight ==
            db.TimesForStation.GetList().Where(j => j.RouteId == endTimesForStation.RouteId && j.DepartureTime < endTimesForStation.ArrivalTime).Count() + 1).First();
            Station startStation = db.Station.GetList().Where(i => i.Id == startStationBusSchedule.IdStation).First();
            Station endStation = db.Station.GetList().Where(i => i.Id == endStationBusSchedule.IdStation).First();
            TicketViewModel ticketViewModel = new TicketViewModel()
            {
                Passenger = passengerForTicket,
                Way = new ConcreteWayBusModel()
                {
                    EndStationModel = new StationModel(endStation),
                    StartStationModel = new StationModel(startStation),
                    EndStationBusScheduleModel = new StationBusScheduleModel(endStationBusSchedule),
                    StartStationBusScheduleModel = new StationBusScheduleModel(startStationBusSchedule),
                    StartTimesForStationModel = new TimesForStationModel(startTimesForStation),
                    EndTimesForStationModel = new TimesForStationModel(endTimesForStation)
                }
            };
            return new TicketModelForProfile(ticketViewModel, ticket);
        }
        public void RemoveTicket(ObservableCollection<TicketModelForProfile> collection, TicketModelForProfile ticket)
        {
            if (collection.FirstOrDefault(i => i == ticket) == null)
                return;
            collection.Remove(ticket);
            Ticket ticketForRemoveFromProfile = db.Ticket.GetItem(ticket.Ticket.Id);
            ticketForRemoveFromProfile.UserId = null;
            db.Ticket.Update(ticketForRemoveFromProfile);
            db.Save();
        }
    }
}
