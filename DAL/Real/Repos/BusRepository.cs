using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Real.Repos
{
    public class BusRepository : IRepositoryBus
    {
        private readonly BusContext db;
        public BusRepository(BusContext db)
        {
            this.db = db;
        }
        public List<Bus> GetList()
        {
            return db.Bus.ToList();
        }
        public Bus GetItem(int id)
        {
            return db.Bus.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Bus item)
        {
            db.Bus.Add(item);
        }
        public void Update(Bus item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Bus bus = GetItem(id);
            if (bus != null)
                db.Bus.Remove(bus);
        }
    }
}
