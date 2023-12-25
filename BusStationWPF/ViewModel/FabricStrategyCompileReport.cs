using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using BusStationWPF.ViewModel.StrategyCompileReport;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel
{
    public static class FabricStrategyCompileReport
    {
        public static IReportCompileStrategy GetStrateguy(FiltersForStrategyCompileReport filters, IUnitOfWork db)
        {
            IReportCompileStrategy strategy;
            if (filters.SearchPassengersMadeAllWay)
                strategy = new StrategyFindPassengersThatMadeAllWay(db);
            else
                strategy = new StrategyIncludesAnyTicketsInWay(db);
            return strategy;
        }
        public static IReportCompileStrategy GetDefault(IUnitOfWork db)
        {
            return new StrategyIncludesAnyTicketsInWay(db);
        }
    }
}
