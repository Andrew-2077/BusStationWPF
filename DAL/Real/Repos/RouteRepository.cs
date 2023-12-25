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
    public class RouteRepository : IRepositoryRoute
    {
        private BusContext db;
        public RouteRepository(BusContext db)
        {
            this.db = db;
        }

        public List<Route> GetList()
        {
            return db.Route.ToList();
        }
        public Route GetItem(int id)
        {
            return db.Route.FirstOrDefault(i => i.Id == id);
        }
        public void Create(Route item)
        {
            db.Route.Add(item);
        }
        public void Update(Route item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Route route = GetItem(id);
            if (route != null)
                db.Route.Remove(route);
        }
    }
}
