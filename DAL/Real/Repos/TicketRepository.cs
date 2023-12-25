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
    public class TicketRepository : IRepositoryTickets
    {
        private readonly BusContext db;
        public TicketRepository(BusContext db)
        {
            this.db = db;
        }
        public List<Ticket> GetList()
        {
            return db.Ticket.ToList();
        }
        public Ticket GetItem(int id)
        {
            return db.Ticket.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Ticket item)
        {
            db.Ticket.Add(item);
        }
        public void Update(Ticket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
