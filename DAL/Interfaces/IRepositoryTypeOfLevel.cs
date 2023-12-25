using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryTypeOfLevel : IRepository<TypeOfLevel>
    {
        TypeOfLevel GetItem(int id);
    }
}
