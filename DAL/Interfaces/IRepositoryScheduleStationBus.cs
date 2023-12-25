using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryScheduleStationBus : IRepository<ScheduleStationBus>
    {
        ScheduleStationBus GetItem(int id);
        void Delete(int id);
    }
}
