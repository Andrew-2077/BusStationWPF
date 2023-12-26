using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Tests
{
    public class SignInTest : ISignIn
    {
        IUnitOfWork _unityOfWork;
        public SignInTest(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public ICommand SignIn
        {
            get => new RelayCommand((obj) =>
            {
                User user = _unityOfWork.Users.GetItem("test");
                UserSignIn.Invoke(new UserModel(user) { TypeUser = BusStationWPF.Model.Enum.TypeUser.Admin });
            });
        }
        public ICommand SignUp
        {
            get => null;
        }
        public ICommand SignOut
        {
            get => new RelayCommand((obj) =>
            {
                UserSignOut?.Invoke();
            });
        }
        public event Action UserSignOut;
        public event Action<UserModel> UserSignIn;
    }
}
