using BusStationWPF.Model.ModelsForGetInfoFromView;
using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace BusStationWPF.ViewModel.StrategiesSearchWay
{
    public class SearchWayStrategyWithMaxTransfer : ISearchWayStrategy
    {
        IUnitOfWork db;
        readonly int MaxCountTransfers = 6;
        public SearchWayStrategyWithMaxTransfer(IUnitOfWork db, int MaxCountTransfers)
        {
            this.db = db;
            this.MaxCountTransfers = Math.Max(0, MaxCountTransfers);
        }
        public List<ConcreteWayFromStationToStation> FindWays(InfoAboutSearchingWaysModel info)
        {
            NodeForSearchWay NodeForSearch = new NodeForSearchWay()
            {
                StationEnd = new StationModel()
                {
                    Id = info.IdEndStation
                },
                PathsForSearching = new Queue<List<BusWayWithoutTime>>(),
                WaysFound = new List<ConcreteWayFromStationToStation>(),
                DateTimeArriving = info.DateTimeArriving,
                CurrentStationAndPathNode = new NodeCurrentStation()
                {
                    CurrentStation = new StationModel()
                    {
                        Id = info.IdStartStation
                    },
                    CurrentPath = new List<BusWayWithoutTime>()
                }
            };
            CheckPathesForStationAndAddFinded(NodeForSearch);
            for (int i = 1; i <= MaxCountTransfers; i++)
                for (int j = NodeForSearch.PathsForSearching.Count - 1; j >= 0; j--)
                {
                    NodeForSearch.CurrentStationAndPathNode = new NodeCurrentStation()
                    {
                        CurrentStation = new StationModel() { Id = NodeForSearch.PathsForSearching.Peek().Last().ToStationSchedule.IdStation },
                        CurrentPath = NodeForSearch.PathsForSearching.Peek()
                    };
                    NodeForSearch.PathsForSearching.Dequeue();
                    CheckPathesForStationAndAddFinded(NodeForSearch);
                }
            return NodeForSearch.WaysFound;
        }
        protected class NodeForSearchWay
        {
            public DateTime DateTimeArriving { get; set; }
            public StationModel StationEnd { get; set; }
            public NodeCurrentStation CurrentStationAndPathNode { get; set; }
            public Queue<List<BusWayWithoutTime>> PathsForSearching { get; set; }
            public List<ConcreteWayFromStationToStation> WaysFound { get; set; }
        }
        protected struct NodeCurrentStation
        {
            public StationModel CurrentStation { get; set; }
            public List<BusWayWithoutTime> CurrentPath { get; set; }
        }
        protected class BusWayWithoutTime
        {
            public ScheduleStationBus FromStationSchedule { get; set; }
            public ScheduleStationBus ToStationSchedule { get; set; }
        }
        protected void CheckPathesForStationAndAddFinded(NodeForSearchWay nodeForSearchWay)
        {
            var WaysFromCurrentStation = (from StationBusSchedule in db.ScheduleStationBus.GetList()
                                          where !nodeForSearchWay.CurrentStationAndPathNode.CurrentPath.Any(i => i.ToStationSchedule.IdBus == StationBusSchedule.IdBus)
                                          group StationBusSchedule by StationBusSchedule.IdBus into AllWaysForCurrentStation
                                          where AllWaysForCurrentStation.FirstOrDefault(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id) != null
                                          select AllWaysForCurrentStation into WaysWithCurrentStation
                                          from StationBusSchedule in WaysWithCurrentStation
                                          where StationBusSchedule.Number_flight >= WaysWithCurrentStation.First(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id).Number_flight
                                          orderby StationBusSchedule.Number_flight
                                          group StationBusSchedule by StationBusSchedule.IdBus
             ).ToList();
            WaysFromCurrentStation.ForEach(Way =>
            {
                CheckWayForFindEndOrNot(nodeForSearchWay, Way);
            });
        }
        void CheckWayForFindEndOrNot(NodeForSearchWay nodeForSearchWay, IGrouping<int, ScheduleStationBus> Way)
        {
            if (Way.FirstOrDefault(i => i.IdStation == nodeForSearchWay.StationEnd.Id) != null)
            {
                List<BusWayWithoutTime> CurrentPath = new List<BusWayWithoutTime>(nodeForSearchWay.CurrentStationAndPathNode.CurrentPath);
                CurrentPath.Add(new BusWayWithoutTime()
                {
                    FromStationSchedule = Way.First(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id),
                    ToStationSchedule = Way.First(i => i.IdStation == nodeForSearchWay.StationEnd.Id)
                });
                CheckAddFoundPathAndAdd(CurrentPath, nodeForSearchWay.DateTimeArriving, nodeForSearchWay);
            }
            else
                AddWaysForSearching(nodeForSearchWay, Way);
        }
        void AddWaysForSearching(NodeForSearchWay nodeForSearchWay, IGrouping<int, ScheduleStationBus> Way)
        {
            List<ScheduleStationBus> ways = Way.ToList();
            for (int i = 1; i < ways.Count; i++)
            {
                List<BusWayWithoutTime> WayForAdd = new List<BusWayWithoutTime>(nodeForSearchWay.CurrentStationAndPathNode.CurrentPath);
                WayForAdd.Add(new BusWayWithoutTime()
                {
                    FromStationSchedule = ways[0],
                    ToStationSchedule = ways[i]
                });
                nodeForSearchWay.PathsForSearching.Enqueue(WayForAdd);
            }
        }
        class TimeForWay
        {
            public Route Route { get; set; }
            public TimesForStation StartTimesForStation { get; set; }
            public TimesForStation EndTimesForStation { get; set; }
            public TimeForWay() { }
            public TimeForWay(Route route, TimesForStation StartTimesForStation, TimesForStation EndTimesForStation)
            {
                Route = route;
                this.StartTimesForStation = StartTimesForStation;
                this.EndTimesForStation = EndTimesForStation;
            }
        }
        void CheckAddFoundPathAndAdd(List<BusWayWithoutTime> Way, DateTime DateTimeArriving, NodeForSearchWay nodeForSearchWay)
        {
            List<TimeForWay> timesForStationsForWay = (
                from routes in db.Route.GetList()
                where Way.FirstOrDefault(i => i.FromStationSchedule.IdBus == routes.IdBus) != null
                join timesForStation in db.TimesForStation.GetList()
                on routes.Id equals timesForStation.RouteId
                orderby timesForStation.DepartureTime
                group timesForStation by routes into timesForRoute
                where db.Ticket.GetList().Where(i => i.IdTimesForStationDestiny == timesForRoute.Key.Id).Count() <
                db.Level.GetList().Where(i => i.BusId == timesForRoute.Key.IdBus).Sum(j => db.Seat.GetList().Where(i => i.TypeOfLevelId == j.TypeOfLevelId).Count())
                select new TimeForWay(timesForRoute.Key,
                timesForRoute.Skip(Way.
                Where(i => i.ToStationSchedule.IdBus == timesForRoute.Key.IdBus).First().FromStationSchedule.Number_flight - 1).First(),
                timesForRoute.Skip(Way.
                Where(i => i.ToStationSchedule.IdBus == timesForRoute.Key.IdBus).First().ToStationSchedule.Number_flight - 1).First()) into TimesBetweenStations
                where TimesBetweenStations.StartTimesForStation.DepartureTime > DateTime.Now && TimesBetweenStations.EndTimesForStation.ArrivalTime <= DateTimeArriving
                orderby TimesBetweenStations.StartTimesForStation.DepartureTime descending
                select TimesBetweenStations
                ).ToList();
            ConcreteWayFromStationToStation FindedWay = FindConcreteWayFromStationToStation(Way, timesForStationsForWay, Way.Count - 1, DateTimeArriving);
            if (FindedWay != null)
            {
                FindedWay.NameStartStation = FindedWay.ConcreteWayBusModels.First().StartStationModel.Name;
                FindedWay.NameEndStation = FindedWay.ConcreteWayBusModels.Last().EndStationModel.Name;
                FindedWay.DepartureTimeStartStation = FindedWay.ConcreteWayBusModels.First().StartTimesForStationModel.DepartureTime;
                FindedWay.ArrivingTimeEndStation = FindedWay.ConcreteWayBusModels.Last().EndTimesForStationModel.ArrivalTime;
                nodeForSearchWay.WaysFound.Add(FindedWay);
            }
        }
        ConcreteWayFromStationToStation FindConcreteWayFromStationToStation(List<BusWayWithoutTime> Way, List<TimeForWay> timeForWays, int IndexInList, DateTime DepartureTimeNextStation)
        {
            if (IndexInList == -1)
                return new ConcreteWayFromStationToStation() { ConcreteWayBusModels = new List<ConcreteWayBusModel>() };
            ConcreteWayFromStationToStation FindedWay = null;
            bool needContinueSearching = true;
            timeForWays.Where(i => i.Route.IdBus == Way[IndexInList].ToStationSchedule.IdBus && i.EndTimesForStation.ArrivalTime < DepartureTimeNextStation).ToList().ForEach(i =>
            {
                if (!needContinueSearching)
                    return;
                FindedWay = FindConcreteWayFromStationToStation(Way, timeForWays, IndexInList - 1, i.StartTimesForStation.DepartureTime);
                if (FindedWay != null)
                {
                    FindedWay.ConcreteWayBusModels.Add(new ConcreteWayBusModel()
                    {
                        EndStationModel = new StationModel(db.Station.GetItem(Way[IndexInList].ToStationSchedule.IdStation)),
                        StartStationModel = new StationModel(db.Station.GetItem(Way[IndexInList].FromStationSchedule.IdStation)),
                        StartStationBusScheduleModel = new StationBusScheduleModel(Way[IndexInList].FromStationSchedule),
                        EndStationBusScheduleModel = new StationBusScheduleModel(Way[IndexInList].ToStationSchedule),
                        StartTimesForStationModel = new TimesForStationModel(i.StartTimesForStation),
                        EndTimesForStationModel = new TimesForStationModel(i.EndTimesForStation)
                    });
                    needContinueSearching = false;
                    return;
                }
            });
            return FindedWay;
        }
    }
}
