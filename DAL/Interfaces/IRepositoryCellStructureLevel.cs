using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entitites;

namespace DAL.Interfaces
{
    public interface IRepositoryCellStructureLevel : IRepository<CellStructureLevel>
    {
        CellStructureLevel GetItem(int NumberOfRow, int NumberOfCell, int TypeOfLevelId);
    }
}
