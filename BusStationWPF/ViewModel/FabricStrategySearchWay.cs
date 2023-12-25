using BusStationWPF.Model.ModelsForGetInfoFromView;
using BusStationWPF.ViewModel.StrategiesSearchWay;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel
{
    public class FabricStrategySearchWay
    {
        IUnitOfWork UnitOfWork;
        public FabricStrategySearchWay(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public SearchWayStrategyWithMaxTransfer GetNewStrategy(InfoAboutFilters info)
        {
            SearchWayStrategyWithMaxTransfer strategy = new SearchWayStrategyWithMaxTransfer(UnitOfWork, info.MaxCountTransfers);
            return strategy;
        }
    }
}
