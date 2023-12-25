using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel
{
    public class ShowStrucureLevelService : IShowerStructureLevel
    {
        private IUnitOfWork db;
        public ShowStrucureLevelService(IUnitOfWork db)
        {
            this.db = db;
        }
        Dictionary<int, List<List<CellStructureLevelModel>>> loadedStructureLevels = new Dictionary<int, List<List<CellStructureLevelModel>>>();
        public void SetStructureLevelWithoutSeats(TypeOfLevelModel level)
        {
            structureLevelWithoutSeats.Clear();
            List<List<CellStructureLevelModel>> finded = GetStrucureLevelModels(level.Id);
            finded.ForEach(i => structureLevelWithoutSeats.Add(i));

        }
        List<List<CellStructureLevelModel>> GetStrucureLevelModels(int IdTypeOfLevel)
        {
            List<List<CellStructureLevelModel>> finded = loadedStructureLevels.FirstOrDefault(i => i.Key == IdTypeOfLevel).Value;
            if (finded == null)
                finded = LoadLevel(IdTypeOfLevel);
            return finded;
        }
        List<List<CellStructureLevelModel>> LoadLevel(int IdTypeOfLevel)
        {
            List<List<CellStructureLevelModel>> Added = new List<List<CellStructureLevelModel>>();
            (from cell in db.CellStructureLevel.GetList().Where(i => i.TypeOfLevelId == IdTypeOfLevel)
             from seat in db.Seat.GetList().Where(i => i.TypeOfLevelId == IdTypeOfLevel && cell.NumberOfSeatInLevel == i.NumberOfSeatInLevel).DefaultIfEmpty()
             group new CellStructureLevelModel(seat?.CostPerStation, cell.NumberOfSeatInLevel, (seat == null ? TypeOccupied.NotSeat : TypeOccupied.Free), seat?.Id) by cell.NumberOfRow into CellSeat
             select CellSeat.ToList()).ToList().ForEach(i => Added.Add(i));
            loadedStructureLevels.Add(IdTypeOfLevel, Added);
            return Added;
        }
        public void SetStrucureWithSeats(List<WayModelForChooseTicket> way)
        {
            structureLevelWithoutSeats.Clear();
            structureLevelsWithSeats.Clear();
            way.ForEach(concreteWayBus =>
            {
                ShowLevelWithNumberInBusMoreThanInParametr(concreteWayBus, 0);
                structureLevelsWithSeats.Add(concreteWayBus);
            });

        }
        void ShowLevelWithNumberInBusMoreThanInParametr(WayModelForChooseTicket concreteWayBus, int NumberInBus)
        {
            concreteWayBus.LevelForShow = new LevelModel(
                ((from level in db.Level.GetList()
                  where level.BusId == concreteWayBus.Way.StartStationBusScheduleModel.IdBus && level.NumberInBus == NumberInBus + 1
                  select level).DefaultIfEmpty()).First());
            List<List<CellStructureLevelModel>> StructureLevelWithoutSeats = GetStrucureLevelModels(concreteWayBus.LevelForShow.TypeOfLevelId);
            List<List<CellStructureLevelModel>> AddedLevel = (
             from cellRow in StructureLevelWithoutSeats
             select (
                (
                from cell in cellRow
                from ticket in (from ticket in db.Ticket.GetList().DefaultIfEmpty()
                                join timesForStation in db.TimesForStation.GetList()
                                    on concreteWayBus.Way.EndTimesForStationModel.RouteId equals timesForStation.RouteId
                                where ticket?.IdTimesForStationSource == timesForStation.Id && cell.SeatId == ticket.IdSeat
                                select ticket).DefaultIfEmpty()
                select new CellStructureLevelModel(cell.CostPerStation, cell.NumberOfSeatInLevel, (cell.typeOccupied == TypeOccupied.NotSeat ? TypeOccupied.NotSeat :
                ticket == null ? TypeOccupied.Free : TypeOccupied.Occupied), cell.SeatId)
                ).ToList()
             )).ToList();
            concreteWayBus.StructureLevelModels.Clear();
            AddedLevel.ForEach(i => concreteWayBus.StructureLevelModels.Add(i));
        }
        public ICommand ShowNextLevel
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is WayModelForChooseTicket way)
                {
                    int MaxNumber = db.Level.GetList().Where(i => i.BusId == way.LevelForShow.BusId).Max(i => i.NumberInBus);
                    ShowLevelWithNumberInBusMoreThanInParametr(way,
                        (way.LevelForShow.NumberInBus < MaxNumber ? way.LevelForShow.NumberInBus : 0));
                }
            });
        }
        public ICommand ShowPreviousLevel
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is WayModelForChooseTicket way)
                {
                    ShowLevelWithNumberInBusMoreThanInParametr(way,
                        (way.LevelForShow.NumberInBus - 2 >= 0 ? way.LevelForShow.NumberInBus - 2 :
                        db.Level.GetList().Where(i => i.BusId == way.LevelForShow.BusId).Max(i => i.NumberInBus) - 1));
                }
            });
        }
        ObservableCollection<List<CellStructureLevelModel>> structureLevelWithoutSeats = new ObservableCollection<List<CellStructureLevelModel>>();
        ObservableCollection<WayModelForChooseTicket> structureLevelsWithSeats = new ObservableCollection<WayModelForChooseTicket>();
        public ObservableCollection<WayModelForChooseTicket> StructureLevelsWithSeats
        {
            get => structureLevelsWithSeats;
        }
        public ObservableCollection<List<CellStructureLevelModel>> StructureLevelWithoutSeats
        {
            get
            {
                return structureLevelWithoutSeats;
            }
        }
    }
}
