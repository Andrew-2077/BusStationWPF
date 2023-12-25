using BusStationWPF.Model.Enum;
using BusStationWPF.ViewModel.EditorsBusDecorators;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Fabrics
{
    public class FabricEditBus
    {
        IUnitOfWork UnitOfWork;
        public FabricEditBus(IUnitOfWork db)
        {
            this.UnitOfWork = db;
        }
        public IProcessDoUndo<Bus> GetProcess(TypeProcess type)
        {
            IProcessDoUndo<Bus> editLevels = new EditorLevel(UnitOfWork, null);
            IProcessDoUndo<Bus> editSchedule = new EditorStationBusSchedule(UnitOfWork, editLevels);
            IProcessDoUndo<Bus> editStartTimes = new EditorStartTime(UnitOfWork, editSchedule);
            return editStartTimes;
        }
    }
}
