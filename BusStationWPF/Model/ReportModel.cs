﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class ReportModel : INotifyPropertyChanged
    {
        ObservableCollection<TicketViewModel> tickets = new ObservableCollection<TicketViewModel>();
        private int countTickets;
        public ObservableCollection<TicketViewModel> Tickets
        {
            get => tickets;
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }
        public int CountTickets
        {
            get => countTickets;
            set
            {
                countTickets = value;
                OnPropertyChanged("CountTickets");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
