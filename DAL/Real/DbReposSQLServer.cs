using DAL.Entitites;
using DAL.Interfaces;
using DAL.Real.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Real
{
    public class DbReposSQLServer : IUnitOfWork
    {
        private readonly BusContext db;
        private UserRepository users;
        private PassengerRepository passengers;
        private CellStructureLevelRepository cellStructureLevel;
        private LevelRepository level;
        private TypeOfLevelRepository typeOflevel;
        private SeatRepository seat;
        private BusRepository bus;
        private StationRepository station;
        private TimesForStationRepository timesForStation;
        private RouteRepository route;
        private StationBusScheduleRepository stationBusSchedule;
        private TicketRepository ticket;
        public DbReposSQLServer(BusContext db)
        {
            this.db = db;
        }

        public IRepositoryUser Users
        {
            get => users ?? (users = new UserRepository(db));
        }
        public IRepositoryPassenger Passengers
        {
            get => passengers ?? (passengers = new PassengerRepository(db));
        }
        public IRepositoryCellStructureLevel CellStructureLevel
        {
            get => cellStructureLevel ?? (cellStructureLevel = new CellStructureLevelRepository(db));
        }
        public IRepositoryLevel Level
        {
            get => level ?? (level = new LevelRepository(db));
        }
        public IRepositorySeat Seat
        {
            get => seat ?? (seat = new SeatRepository(db));
        }
        public IRepositoryBus Bus
        {
            get => bus ?? (bus = new BusRepository(db));
        }
        public IRepositoryTypeOfLevel TypeOfLevel
        {
            get => typeOflevel ?? (typeOflevel = new TypeOfLevelRepository(db));
        }
        public IRepositoryStation Station
        {
            get => station ?? (station = new StationRepository(db));
        }
        public IRepositoryTimesForStation TimesForStation
        {
            get => timesForStation ?? (timesForStation = new TimesForStationRepository(db));
        }
        public IRepositoryRoute Route
        {
            get => route ?? (route = new RouteRepository(db));
        }
        public IRepositoryStationBusSchedule StationBusSchedule
        {
            get => stationBusSchedule ?? (stationBusSchedule = new StationBusScheduleRepository(db));
        }
        public IRepositoryTickets Ticket
        {
            get => ticket ?? (ticket = new TicketRepository(db));
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
