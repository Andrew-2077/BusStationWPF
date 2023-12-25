using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryLevel : IRepository<Level>
    {
        Level GetItem(int id);
        void Delete(int id);
    }
}
