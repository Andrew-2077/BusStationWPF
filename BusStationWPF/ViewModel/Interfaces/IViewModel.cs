using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IViewModel
    {
        INavigationService NavigationService { get; }
        ISignIn SignInService { get; }
        IInfoProfile InfoProfileService { get; }
        IEditorBus EditorBusService { get; }
        IShowerStructureLevel ShowerStructureLevel { get; }
        ISearcherWaysService SearcherWaysService { get; }
        IBuyTicket BuyTicketService { get; }
        IMainMenuController MainMenuControllerService { get; }
        IReportService ReportService { get; }
        IGetCollectionsService GetCollectionsService { get; }
        IProcessDoUndo<Bus> EditorStrategy { get; }
    }
}
