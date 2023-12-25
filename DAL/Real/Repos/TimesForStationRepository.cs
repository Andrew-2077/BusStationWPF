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
    public class TimesForStationRepository : IRepositoryTimesForStation
    {
        private BusContext db;
        public TimesForStationRepository(BusContext db)
        {
            this.db = db;
        }
        public List<TimesForStation> GetList()
        {
            return db.TimesForStation.ToList();
        }
        public TimesForStation GetItem(int id)
        {
            return db.TimesForStation.FirstOrDefault(i => i.Id == id);
        }
        public void Create(TimesForStation item)
        {
            db.TimesForStation.Add(item);
        }
        public void Update(TimesForStation item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TimesForStation timesForStation = GetItem(id);
            if (timesForStation != null)
                db.TimesForStation.Remove(timesForStation);
        }
    }
}
