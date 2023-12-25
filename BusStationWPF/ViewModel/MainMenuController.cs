using BusStationWPF.Model.Enum;
using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces.AbstractClass;
using BusStationWPF.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel
{
    public class MainMenuController : IMainMenuController
    {
        SetterVisibleButtonsMainMenu setterVisibleButtonsMainMenu;
        public event Action<TypePage> UserChoosePage;
        public MenuShow VisibleButtons { get; } = new MenuShow();
        public MainMenuController(SetterVisibleButtonsMainMenu setterVisibleButtonsMainMenu)
        {
            this.setterVisibleButtonsMainMenu = setterVisibleButtonsMainMenu;
            SetUserSignOut();
        }
        public void SetUserSignIn(UserModel user)
        {
            setterVisibleButtonsMainMenu.SetVisibleForButtons(VisibleButtons, user.TypeUser);
        }
        public void SetUserSignOut()
        {
            VisibleButtons.VisibleBuyTicket = "Collapsed";
            VisibleButtons.VisibleCreateBus = "Collapsed";
            VisibleButtons.VisibleProfile = "Collapsed";
            VisibleButtons.VisibleSignIn = "Visible";
            VisibleButtons.VisibleSignOut = "Collapsed";
            VisibleButtons.VisibleSignUp = "Visible";
            VisibleButtons.VisibleReport = "Collapsed";
        }
        public ICommand LoadProfile
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.ProfilePage));
        }
        public ICommand LoadSearchWay
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.SearchWayPageBuyTicket));
        }
        public ICommand LoadEditBus
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.EditLevelPage));
        }
        public ICommand LoadReports
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.SearchWayPageReport));
        }
    }
}
