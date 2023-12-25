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
    public class TypeOfLevelRepository : IRepositoryTypeOfLevel
    {
        private readonly BusContext db;
        public TypeOfLevelRepository(BusContext db)
        {
            this.db = db;
        }
        public List<TypeOfLevel> GetList()
        {
            return db.TypeOfLevel.ToList();
        }
        public TypeOfLevel GetItem(int id)
        {
            return db.TypeOfLevel.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(TypeOfLevel item)
        {
            db.TypeOfLevel.Add(item);
        }
        public void Update(TypeOfLevel item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
