using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BusStationWPF.ViewModel
{
    public class BuyTicketService : IBuyTicket
    {
        IUnitOfWork db;
        private UserModel user;
        public void SetUser(UserModel user) => this.user = user;
        public event Action TicketsPurchased;
        public event Action CancelProcess;
        IChooseTicketService chooseTicketService;
        public IChooseTicketService ChooseTicketService { get => chooseTicketService; }
        public BuyTicketService(IUnitOfWork db)
        {
            this.db = db;
        }
        List<TicketViewModel> tickets = new List<TicketViewModel>();
        public List<TicketViewModel> Tickets { get => tickets; }
        public void SetChooseTicketService()
        {
            FabricChooseTicket fabric = new FabricChooseTicket(user, db);
            chooseTicketService = fabric.GetChooseTicketService();
            ChooseTicketService.ProcessComplete += AddTicketsToUser;
        }
        void AddTicketsToUser(List<Ticket> tickets)
        {
            foreach (Ticket ticket in tickets)
            {
                ticket.UserId = user.Id;
                db.Ticket.Create(ticket);
            }
            db.Save();
            this.tickets = tickets.Select(i =>
            {
                Route CurrentRoute = (from timesForStation in db.TimesForStation.GetList()
                                      where timesForStation.Id == i.IdTimesForStationSource
                                      select timesForStation.Route).First();
                TimesForStation startTime = db.TimesForStation.GetItem(i.IdTimesForStationSource);
                TimesForStation endTime = db.TimesForStation.GetItem(i.IdTimesForStationDestiny);
                ScheduleStationBus StartScheduleModel = (
                from stationBusSchedule in db.ScheduleStationBus.GetList()
                where stationBusSchedule.Number_flight ==
                (from timesForStation in db.TimesForStation.GetList()
                 where timesForStation.RouteId == startTime.RouteId
                 && startTime.ArrivalTime > timesForStation.DepartureTime
                 select timesForStation).Count() + 1
                select stationBusSchedule).First();
                ScheduleStationBus EndScheduleModel = (
                from stationBusSchedule in db.ScheduleStationBus.GetList()
                where stationBusSchedule.Number_flight ==
                (from timesForStation in db.TimesForStation.GetList()
                 where timesForStation.RouteId == endTime.RouteId
                 && endTime.ArrivalTime > timesForStation.DepartureTime
                 select timesForStation).Count() + 1
                select stationBusSchedule).First();
                Station startStation = db.Station.GetItem(StartScheduleModel.IdStation);
                Station endStation = db.Station.GetItem(EndScheduleModel.IdStation);
                return new TicketViewModel()
                {
                    Way = new ConcreteWayBusModel()
                    {
                        EndStationModel = new StationModel(endStation),
                        StartStationModel = new StationModel(startStation),
                        StartStationBusScheduleModel = new StationBusScheduleModel(StartScheduleModel),
                        EndStationBusScheduleModel = new StationBusScheduleModel(EndScheduleModel),
                        StartTimesForStationModel = new TimesForStationModel(startTime),
                        EndTimesForStationModel = new TimesForStationModel(endTime)
                    },
                    Passenger = new PassengerViewModel(db.Passengers.GetItem(i.Passenger.Id), true)
                };
            }
            ).ToList();
            TicketsPurchased?.Invoke();
        }
        public ICommand CancelCurrentProcess
        {
            get => new RelayCommand((obj) =>
            {
                CancelProcess?.Invoke();
                chooseTicketService.CancelCurrentProcessTicket();
            });
        }
        public ICommand StartTicketProcessing
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is ConcreteWayFromStationToStation way)
                {
                    SetChooseTicketService();
                    List<WayModelForChooseTicket> ways = way.ConcreteWayBusModels.Select(i => new WayModelForChooseTicket() { Way = i }).ToList();
                    ChooseTicketService.SetConcreteWayFromStationToStation(ways);
                }
            }, (obj) => user != null);
        }
    }
}
