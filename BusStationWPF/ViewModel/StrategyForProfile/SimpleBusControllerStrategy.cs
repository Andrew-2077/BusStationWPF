using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.StrategyForProfile
{
    public class SimpleBusControllerStrategy : IBusInProfileStrategy
    {
        IUnitOfWork db;
        UserModel user;
        public SimpleBusControllerStrategy(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetUser(UserModel user)
        {
            this.user = user;
        }
        public List<BusInProfileModel> LoadBuses()
        {
            return (
            from bus in db.Bus.GetList()
            where bus.IdUserCreator == user.Id
            select new BusInProfileModel
            {
                BusModel = new BusModel() { Id = bus.Id, IdUserCreator = (int)bus.IdUserCreator, LoadedInDB = true },
                Stations = (from stationbusschedule in db.ScheduleStationBus.GetList()
                            where bus.Id == stationbusschedule.IdBus
                            join station in db.Station.GetList()
                            on stationbusschedule.IdStation equals station.Id
                            orderby stationbusschedule.Number_flight
                            select station.Name).ToList()
            }).ToList();
        }
        public void RemoveBus(ObservableCollection<BusInProfileModel> collection, BusInProfileModel bus)
        {
            if (collection.FirstOrDefault(i => i == bus) == null)
                return;
            collection.Remove(bus);
            Bus busForRemove = db.Bus.GetItem(bus.BusModel.Id);
            busForRemove.IdUserCreator = null;
            db.Route.GetList().Where(i => i.IdBus == busForRemove.Id).ToList().ForEach(i =>
            {
                List<TimesForStation> timesForStations = db.TimesForStation.GetList().Where(j => j.RouteId == i.Id).ToList();
                if (db.Ticket.GetList().All(j => timesForStations.FirstOrDefault(k => k.Id == j.IdTimesForStationSource) == null))
                {
                    db.Route.Delete(i.Id);
                }
            });
            db.Save();
        }
        public void EditBus(BusInProfileModel bus)
        {

        }
    }
}
