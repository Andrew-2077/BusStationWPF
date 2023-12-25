using BusStationWPF.Model.Enum;
using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces.AbstractClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel
{
    public class SetterVisibleButtonsMenuShowAdminAndSimpleUser : SetterVisibleButtonsMainMenu
    {
        public override void SetVisibleForButtons(MenuShow menuShow, TypeUser type)
        {
            if (type == TypeUser.Admin)
                SetWorkerUserSignIn(menuShow);
            else
                SetSimpleUserSignIn(menuShow);

        }
        protected void SetSimpleUserSignIn(MenuShow menuShow)
        {
            menuShow.VisibleBuyTicket = "Visible";
            menuShow.VisibleCreateBus = "Visible";
            menuShow.VisibleProfile = "Visible";
            menuShow.VisibleSignIn = "Collapsed";
            menuShow.VisibleSignOut = "Visible";
            menuShow.VisibleSignUp = "Collapsed";
            menuShow.VisibleReport = "Collapsed";
        }
        protected void SetWorkerUserSignIn(MenuShow menuShow)
        {
            menuShow.VisibleBuyTicket = "Visible";
            menuShow.VisibleCreateBus = "Visible";
            menuShow.VisibleProfile = "Visible";
            menuShow.VisibleSignIn = "Collapsed";
            menuShow.VisibleSignOut = "Visible";
            menuShow.VisibleSignUp = "Collapsed";
            menuShow.VisibleReport = "Visible";
        }
    }
}
