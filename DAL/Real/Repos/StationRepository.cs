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
    public class StationRepository : IRepositoryStation
    {
        private BusContext db;
        public StationRepository(BusContext db)
        {
            this.db = db;
        }

        public List<Station> GetList()
        {
            return db.Station.ToList();
        }
        public Station GetItem(int id)
        {
            return db.Station.FirstOrDefault(i => i.Id == id);
        }
        public void Create(Station item)
        {
            db.Station.Add(item);
        }
        public void Update(Station item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
