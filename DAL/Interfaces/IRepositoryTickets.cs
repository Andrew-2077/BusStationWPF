using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryTickets : IRepository<Ticket>
    {
        Ticket GetItem(int id);
    }
}
