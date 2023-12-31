﻿using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IBuyTicket
    {
        void SetUser(UserModel user);
        event Action TicketsPurchased;
        event Action CancelProcess;
        List<TicketViewModel> Tickets { get; }
        IChooseTicketService ChooseTicketService { get; }
        ICommand StartTicketProcessing { get; }
        ICommand CancelCurrentProcess { get; }
    }
}
