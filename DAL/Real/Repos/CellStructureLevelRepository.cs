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
    public class CellStructureLevelRepository : IRepositoryCellStructureLevel
    {
        private BusContext db;
        public CellStructureLevelRepository(BusContext db)
        {
            this.db = db;
        }

        public List<CellStructureLevel> GetList()
        {
            return db.CellStructureLevel.ToList();
        }
        public CellStructureLevel GetItem(int NumberOfRow, int NumberOfCell, int TypeOfLevelId)
        {
            return db.CellStructureLevel.FirstOrDefault(i => i.NumberOfRow == NumberOfRow && i.NumberOfCell == NumberOfCell && i.TypeOfLevelId == TypeOfLevelId);
        }
        public void Create(CellStructureLevel item)
        {
            db.CellStructureLevel.Add(item);
        }
        public void Update(CellStructureLevel item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
