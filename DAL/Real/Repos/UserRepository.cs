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
    public class UserRepository : IRepositoryUser
    {
        private readonly BusContext db;
        public UserRepository(BusContext db)
        {
            this.db = db;
        }
        public List<User> GetList()
        {
            return db.User.ToList();
        }
        public User GetItem(int id)
        {
            return db.User.Where(i => i.Id == id).FirstOrDefault();
        }
        public User GetItem(string Login)
        {
            return db.User.Where(i => i.Login == Login).FirstOrDefault();
        }
        public void Create(User item)
        {
            db.User.Add(item);
        }
        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            User user;
            if ((user = GetItem(id)) != null)
                db.User.Remove(user);
        }
    }
}
