using BusStationWPF.ViewModel.DecorateTicketService;
using BusStationWPF.ViewModel.Interfaces.AbstractClass;
using BusStationWPF.ViewModel.Interfaces;
using BusStationWPF.ViewModel.StrategiesSearchWay;
using BusStationWPF.ViewModel.StrategyForProfile;
using BusStationWPF.ViewModel;
using DAL.Interfaces;
using DAL.Real;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BusStationWPF.ViewModel.Tests;

namespace BusStationWPF.Util
{
    internal class NinjectRegistrations : NinjectModule
    {
        private readonly MainWindow window;
        public NinjectRegistrations(MainWindow window)
        {
            this.window = window;
        }
        public override void Load()
        {
            Bind<IShowerStructureLevel>().To<ShowStrucureLevelService>();
            Bind<ISignIn>().To<SignInTest>().InSingletonScope();
            Bind<IEditorBus>().To<EditorBusService>().InSingletonScope();
            Bind<INavigationService>().To<NavigationService>().InSingletonScope().WithConstructorArgument<Frame>(window.NavigableFrame);
            Bind<IInfoProfile>().To<ProfileService>().InSingletonScope();
            Bind<IUnitOfWork>().To<DbReposSQLServer>().InSingletonScope();
            Bind<IViewModel>().To<ViewModelUnit>().InSingletonScope();
            Bind<ISearcherWaysService>().To<SearcherWaysService>().InSingletonScope();
            Bind<IBuyTicket>().To<BuyTicketService>().InSingletonScope();
            Bind<IMainMenuController>().To<MainMenuController>().InSingletonScope();
            Bind<SetterVisibleButtonsMainMenu>().To<SetterVisibleButtonsMenuShowAdminAndSimpleUser>();
            Bind<ISearchWayStrategy>().To<SearchWayStrategyWithMaxTransfer>().WithConstructorArgument(6);
            Bind<IChooseTicketService>().To<ChooseTicketService>();
            Bind<IDecoratorChooseTicketService>().To<FillPassengersForTicketService>();
            Bind<IReportService>().To<ReportService>();
            Bind<IStrategyAddPassengerForProfile>().To<SimpleValidateBeforeAddPassengerStrategy>();
            Bind<ITicketContolStrategyInProfile>().To<SimpleTicketControlStrategy>();
            Bind<IBusInProfileStrategy>().To<SimpleBusControllerStrategy>();
            Bind<IGetCollectionsService>().To<GetCollectionsService>();
        }
    }
}
