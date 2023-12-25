using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryUser : IRepository<User>
    {
        User GetItem(int id);
        User GetItem(string Login);
        void Delete(int id);

    }
}
