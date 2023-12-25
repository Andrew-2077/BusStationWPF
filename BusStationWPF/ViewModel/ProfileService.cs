using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusStationWPF.ViewModel
{
    public class ProfileService : IInfoProfile
    {
        private UserModel currentUser;
        private IUnitOfWork unitOfWork;
        IStrategyAddPassengerForProfile strategyAddPassengerForProfile;
        ITicketContolStrategyInProfile ticketContolStrategyInProfile;
        IBusInProfileStrategy busStrategy;
        public void SetCurrentUser(UserModel user)
        {
            currentUser = user;
            if (strategyAddPassengerForProfile != null)
                this.strategyAddPassengerForProfile.SetUser(currentUser);
            if (this.ticketContolStrategyInProfile != null)
                this.ticketContolStrategyInProfile.SetUser(currentUser);
            if (this.busStrategy != null)
                this.busStrategy.SetUser(currentUser);
        }
        public void SetStrategyAddPassenger(IStrategyAddPassengerForProfile strategyAddPassengerForProfile)
        {
            this.strategyAddPassengerForProfile = strategyAddPassengerForProfile;
            if (currentUser != null)
                this.strategyAddPassengerForProfile.SetUser(currentUser);
        }
        public void SetStrategyTikcetControl(ITicketContolStrategyInProfile ticketContolStrategy)
        {
            this.ticketContolStrategyInProfile = ticketContolStrategy;
            if (currentUser != null)
                this.ticketContolStrategyInProfile.SetUser(currentUser);
        }
        public void SetStrategyBusControl(IBusInProfileStrategy busStrategy)
        {
            this.busStrategy = busStrategy;
            if (currentUser != null)
                this.busStrategy.SetUser(currentUser);
        }
        public void ClearDate()
        {
            tickets.Clear();
            busInProfileModels.Clear();
            passengers.Clear();
        }
        public void LoadDataForProfile()
        {
            if (currentUser == null)
                return;
            ClearDate();
            List<PassengerViewModel> loadedPassengers = strategyAddPassengerForProfile.LoadPassengers();
            loadedPassengers.ForEach(i => passengers.Add(i));
            List<TicketModelForProfile> loadedTickets = ticketContolStrategyInProfile.LoadTickets();
            loadedTickets.ForEach(i => tickets.Add(i));
            List<BusInProfileModel> loadedBuses = busStrategy.LoadBuses();
            loadedBuses.ForEach(i => busInProfileModels.Add(i));

        }
        private ObservableCollection<PassengerViewModel> passengers = new ObservableCollection<PassengerViewModel>();
        private ObservableCollection<TicketModelForProfile> tickets = new ObservableCollection<TicketModelForProfile>();
        public ObservableCollection<TicketModelForProfile> Tickets { get => tickets; }
        private ObservableCollection<BusInProfileModel> busInProfileModels = new ObservableCollection<BusInProfileModel>();
        public ObservableCollection<BusInProfileModel> BusInProfileModels
        {
            get
            {
                return busInProfileModels;
            }
        }
        public ObservableCollection<PassengerViewModel> PassengerViewModels
        {
            get
            {
                return passengers;
            }
        }
        public ProfileService(IUnitOfWork unityOfWork, IStrategyAddPassengerForProfile strategyAddPassengerForProfile, ITicketContolStrategyInProfile tickeStrategy, IBusInProfileStrategy busStrategy)
        {
            this.unitOfWork = unityOfWork;
            SetStrategyAddPassenger(strategyAddPassengerForProfile);
            SetStrategyTikcetControl(tickeStrategy);
            this.busStrategy = busStrategy;
        }
        public ICommand EditBus
        {
            get => new RelayCommand(obj =>
            {
                if (obj is BusInProfileModel busInProfileModelForEdit)
                {
                    busStrategy.EditBus(busInProfileModelForEdit);
                }
            });
        }
        public ICommand AddPassenger
        {
            get => new RelayCommand((obj) =>
            {
                strategyAddPassengerForProfile.AddPassenger(passengers);
            });
        }
        public ICommand RemovePassenger
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForRemove)
                {
                    strategyAddPassengerForProfile.Remove(passengers, PassangerForRemove);
                }
            });
        }
        public ICommand SavePassenger
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForSave)
                {
                    strategyAddPassengerForProfile.Save(PassangerForSave);
                }
            }, (obj) =>
            {
                if (obj is PassengerViewModel PassangerForSave)
                    return strategyAddPassengerForProfile.Validate(PassangerForSave);
                return false;
            });
        }
        public ICommand ChangePassword
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PasswordChangeModel passwordChangeModel && currentUser.Password == passwordChangeModel.OldPassword)
                {
                    currentUser.Password = passwordChangeModel.NewPassword;
                    User user = unitOfWork.Users.GetItem(currentUser.Id);
                    unitOfWork.Users.Update(user);
                    unitOfWork.Save();
                    MessageBox.Show("Пароль успешно сменен!");

                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            },
                (obj) =>
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public ICommand RemoveBus
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is BusInProfileModel busInProfileModelForRemove)
                {
                    busStrategy.RemoveBus(busInProfileModels, busInProfileModelForRemove);
                }
            });
        }
        public ICommand RemoveTicket
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is TicketModelForProfile ticketModel)
                {
                    ticketContolStrategyInProfile.RemoveTicket(tickets, ticketModel);
                }
            });
        }
    }
}
