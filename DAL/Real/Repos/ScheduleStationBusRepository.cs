using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Real.Repos
{
    public class ScheduleStationBusRepository : IRepositoryScheduleStationBus
    {
        private BusContext db;
        public ScheduleStationBusRepository(BusContext db)
        {
            this.db = db;
        }
        public List<StationBusSchedule> GetList()
        {
            return db.ScheduleStationBus.ToList();
        }
        public StationBusSchedule GetItem(int id)
        {
            return db.ScheduleStationBus.FirstOrDefault(i => i.Id == id);
        }
        public void Create(StationBusSchedule item)
        {
            db.ScheduleStationBus.Add(item);
        }
        public void Update(StationBusSchedule item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            StationBusSchedule stationBusSchedule = GetItem(id);
            if (stationBusSchedule != null)
                db.ScheduleStationBus.Remove(stationBusSchedule);
        }
    }
}
