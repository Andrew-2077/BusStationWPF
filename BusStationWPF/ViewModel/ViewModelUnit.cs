using BusStationWPF.Model.Enum;
using BusStationWPF.ViewModel.DecorateTicketService;
using BusStationWPF.ViewModel.EditorsBusDecorators;
using BusStationWPF.ViewModel.Fabrics;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationWPF.View;

namespace BusStationWPF.ViewModel
{
    public class ViewModelUnit : IViewModel
    {
        public ViewModelUnit
            (ISignIn signIn,
            IInfoProfile infoProfile,
            INavigationService navigationService,
            IEditorBus editorBus,
            IShowerStructureLevel showerStructureLevel,
            ISearcherWaysService searcherWays,
            IBuyTicket buyTicket,
            IMainMenuController mainMenuController,
            IReportService reportService,

        IGetCollectionsService GetCollectionsService,
        FabricEditBus fabricEditBus)
        {
            this.SignInService = signIn;
            this.InfoProfileService = infoProfile;
            this.NavigationService = navigationService;
            this.EditorBusService = editorBus;
            this.ShowerStructureLevel = showerStructureLevel;
            this.SearcherWaysService = searcherWays;
            this.BuyTicketService = buyTicket;
            this.MainMenuControllerService = mainMenuController;
            this.ReportService = reportService;
            this.GetCollectionsService = GetCollectionsService;

            EditorBusService.SetCreatorBus(fabricEditBus.GetProcess(TypeProcess.EditAllBus));

            #region SignIn

            #endregion

            #region ReportService
            #endregion

            #region SearcherWaysService
            NavigationService.Leave += (page) =>
            {
                if (page is SearchWaysForBuyTicketPage)
                    SearcherWaysService.ClearResults();
            };
            NavigationService.Leave += (page) =>
            {
                if (page is SearchWaysForReportPage)
                    SearcherWaysService.ClearResults();
            };
            #endregion

            #region BuyTicket
            SignInService.UserSignIn += BuyTicketService.SetUser;
            #endregion

            #region ShowerStructureLevel
            EditorLevel.SelectedLevelChanged += ShowerStructureLevel.SetStructureLevelWithoutSeats;
            ChooseTicketService.ShowNewWay += ShowerStructureLevel.SetStrucureWithSeats;
            #endregion

            #region InfoProfile
            //SignInService.UserSignOut += InfoProfileService.CurrentUserSignOut;
            SignInService.UserSignIn += InfoProfileService.SetCurrentUser;
            NavigationService.Leave += (page) =>
            {
                if (page is Profile)
                    InfoProfileService.ClearDate();
            };
            NavigationService.Enter += (page) =>
            {
                if (page is Profile)
                    InfoProfileService.LoadDataForProfile();
            };
            #endregion

            #region NavigationService
            EditorStartTime.StartProcessLevels += () => NavigationService.LoadNextPage(TypePage.EditStartTimeBus);
            EditorStationBusSchedule.StartProcessLevels += () => NavigationService.LoadNextPage(TypePage.EditScheduleBus);
            EditorLevel.StartProcessLevels += () => NavigationService.LoadNextPage(TypePage.EditLevelPage);
            EditorBusService.CancelCurrentProcess += NavigationService.LoadPreviousPage;
            ReportService.ReportReady += () => NavigationService.LoadNextPage(TypePage.ReportPage);
            ReportService.ShowReportEnded += () => NavigationService.LoadPreviousPage();
            MainMenuControllerService.UserChoosePage += NavigationService.LoadPageWithNotify;
            EditorBusService.BusSaved += () => NavigationService.LoadPageWithNotify(TypePage.ProfilePage);
            ChooseTicketService.ShowNewWay += (obj) => NavigationService.LoadNextPage(TypePage.ChooseTicketPage);
            FillPassengersForTicketService.UserStartFillPassenger += () => NavigationService.LoadNextPage(TypePage.FillPassengerForBuyTicketPage);
            BuyTicketService.TicketsPurchased += () =>
            {
                SearcherWaysService.ClearResults();
                NavigationService.ClearHistoryPage();
                NavigationService.LoadPage(TypePage.FinalPageBuyTicket);
            };
            BuyTicketService.CancelProcess += NavigationService.LoadPreviousPage;
            NavigationService.SetViewModel(this);
            #endregion

            #region EditorBus
            SignInService.UserSignIn += EditorBusService.SetUser;
            #endregion

            #region MainMenuController
            SignInService.UserSignOut += MainMenuControllerService.SetUserSignOut;
            SignInService.UserSignIn += MainMenuControllerService.SetUserSignIn;
            #endregion
        }
        public IEditorBus EditorBusService { get; }
        public ISignIn SignInService { get; private set; }
        public IInfoProfile InfoProfileService { get; protected set; }
        public INavigationService NavigationService { get; protected set; }
        public IShowerStructureLevel ShowerStructureLevel { get; }
        public ISearcherWaysService SearcherWaysService { get; }
        public IBuyTicket BuyTicketService { get; }
        public IMainMenuController MainMenuControllerService { get; }
        public IReportService ReportService { get; }
        public IGetCollectionsService GetCollectionsService { get; }
        public IProcessDoUndo<Bus> EditorStrategy { get; }
    }
}
