using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IInfoProfile
    {
        void SetCurrentUser(UserModel user);
        ObservableCollection<PassengerViewModel> PassengerViewModels { get; }
        ObservableCollection<BusInProfileModel> BusInProfileModels { get; }
        ObservableCollection<TicketModelForProfile> Tickets { get; }
        void ClearDate();
        void LoadDataForProfile();
        ICommand EditBus { get; }
        ICommand RemoveBus { get; }
        ICommand ChangePassword { get; }
        ICommand AddPassenger { get; }
        ICommand RemovePassenger { get; }
        ICommand SavePassenger { get; }
        ICommand RemoveTicket { get; }
    }
}
