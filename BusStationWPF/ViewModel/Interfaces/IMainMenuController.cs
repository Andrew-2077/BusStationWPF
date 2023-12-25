using BusStationWPF.Model.Enum;
using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IMainMenuController
    {
        event Action<TypePage> UserChoosePage;
        MenuShow VisibleButtons { get; }
        void SetUserSignIn(UserModel user);
        void SetUserSignOut();
        ICommand LoadProfile { get; }
        ICommand LoadSearchWay { get; }
        ICommand LoadEditBus { get; }
        ICommand LoadReports { get; }
    }
}
