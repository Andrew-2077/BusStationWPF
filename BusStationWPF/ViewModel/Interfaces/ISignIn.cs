using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface ISignIn
    {
        event Action UserSignOut;
        event Action<UserModel> UserSignIn;
        ICommand SignIn { get; }
        ICommand SignUp { get; }
        ICommand SignOut { get; }
    }
}
