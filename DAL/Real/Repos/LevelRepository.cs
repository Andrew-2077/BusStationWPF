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
    public class LevelRepository : IRepositoryLevel
    {
        private readonly BusContext db;
        public LevelRepository(BusContext db)
        {
            this.db = db;
        }
        public List<Level> GetList()
        {
            return db.Level.ToList();
        }
        public Level GetItem(int id)
        {
            return db.Level.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Level item)
        {
            db.Level.Add(item);
        }
        public void Update(Level item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Level level = GetItem(id);
            if (level != null)
                db.Level.Remove(level);
        }
    }
}
