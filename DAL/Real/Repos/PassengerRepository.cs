using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Real.Repos
{
    public class PassengerRepository : IRepositoryPassenger
    {
        private BusContext db;
        public PassengerRepository(BusContext db)
        {
            this.db = db;
        }

        public List<Passenger> GetList()
        {
            return db.Passenger.ToList();
        }
        public Passenger GetItem(int id)
        {
            return db.Passenger.FirstOrDefault(i => i.Id == id);
        }
        public void Create(Passenger item)
        {
            db.Passenger.Add(item);
        }
        public void Update(Passenger item)
        {
            Passenger UpdatePassenger = GetItem(item.Id);
            if (UpdatePassenger != null)
                db.Entry(UpdatePassenger).CurrentValues.SetValues(item);
        }
        public void Delete(int id)
        {
            Passenger passenger = GetItem(id);
            if (passenger != null)
                db.Passenger.Remove(passenger);
        }
    }
}
