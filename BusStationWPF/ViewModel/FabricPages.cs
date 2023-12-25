using BusStationWPF.Model.Enum;
using BusStationWPF.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BusStationWPF.View;

namespace BusStationWPF.ViewModel
{
    public class FabricPages
    {
        IViewModel viewModel;
        public FabricPages(IViewModel view)
        {
            this.viewModel = view;
        }
        public Page GetPage(TypePage typePage)
        {
            Page PageForCreate = null;
            if (typePage == TypePage.ProfilePage)
                PageForCreate = new Profile(viewModel);
            else if (typePage == TypePage.EditBusPage)
                PageForCreate = new BusEditPage(viewModel);
            else if (typePage == TypePage.ChooseTicketPage)
                PageForCreate = new ChooseSeatsPage(viewModel);
            else if (typePage == TypePage.SearchWayPageBuyTicket)
                PageForCreate = new SearchWaysForBuyTicketPage(viewModel);
            else if (typePage == TypePage.FillPassengerForBuyTicketPage)
                PageForCreate = new FillPassengerForTickets(viewModel);
            else if (typePage == TypePage.FinalPageBuyTicket)
                PageForCreate = new FinalPageBuyTicket(viewModel);
            else if (typePage == TypePage.SearchWayPageReport)
                PageForCreate = new SearchWaysForReportPage(viewModel);
            else if (typePage == TypePage.ReportPage)
                PageForCreate = new ReportPage(viewModel);
            else if (typePage == TypePage.EditLevelPage)
                PageForCreate = new EditLevelPage(viewModel);
            else if (typePage == TypePage.EditScheduleBus)
                PageForCreate = new EditStationBusSchedulePage(viewModel);
            else if (typePage == TypePage.EditStartTimeBus)
                PageForCreate = new EditStartTimePage(viewModel);
            return PageForCreate;
        }
    }
}
