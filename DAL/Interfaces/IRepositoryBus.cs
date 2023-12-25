using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryBus : IRepository<Bus>
    {
        Bus GetItem(int id);
        void Delete(int id);
    }
}
