using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryUser Users { get; }
        IRepositoryPassenger Passengers { get; }
        IRepositoryCellStructureLevel CellStructureLevel { get; }
        IRepositoryBus Bus { get; }
        IRepositorySeat Seat { get; }
        IRepositoryTypeOfLevel TypeOfLevel { get; }
        IRepositoryLevel Level { get; }
        IRepositoryStation Station { get; }
        IRepositoryTimesForStation TimesForStation { get; }
        IRepositoryRoute Route { get; }
        IRepositoryStationBusSchedule StationBusSchedule { get; }
        IRepositoryTickets Ticket { get; }
        int Save();

    }
}
