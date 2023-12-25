using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entitites;

namespace BusStationWPF.ViewModel.StrategyCompileReport
{
    public class StrategyFindPassengersThatMadeAllWay : IReportCompileStrategy
    {
        IUnitOfWork db;
        public StrategyFindPassengersThatMadeAllWay(IUnitOfWork db)
        {
            this.db = db;
        }

        public ReportModel CompileReport(ConcreteWayFromStationToStation way)
        {
            ReportModel reportModel = new ReportModel();
            db.Passengers.GetList().ForEach(passenger =>
            {
                PassengerViewModel passengerViewModel = new PassengerViewModel(passenger, true);
                List<Ticket> tickets = db.Ticket.GetList().Where(i => i.IdPassenger == passenger.Id).ToList();
                if (way.ConcreteWayBusModels.All(i => tickets.FirstOrDefault(j => j.IdTimesForStationSource == i.StartTimesForStationModel.Id
                && j.IdTimesForStationDestiny == i.EndTimesForStationModel.Id) != null))
                    way.ConcreteWayBusModels.ForEach(i => reportModel.Tickets.Add(new TicketViewModel()
                    {
                        Passenger = passengerViewModel,
                        Way = i
                    }));
            });
            return reportModel;
        }
    }
}
