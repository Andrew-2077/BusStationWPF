using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryRoute : IRepository<Route>
    {
        Route GetItem(int id);
        void Delete(int id);
    }
}
