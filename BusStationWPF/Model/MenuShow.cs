using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class MenuShow : INotifyPropertyChanged
    {
        private string visibleProfile;
        private string visibleBuyTicket;
        private string visibleCreateBus;
        private string visibleSignOut;
        private string visibleSignIn;
        private string visibleSignUp;
        private string visibleReport;
        public string VisibleProfile
        {
            get => visibleProfile;
            set
            {
                visibleProfile = value;
                OnPropertyChanged("VisibleProfile");
            }
        }
        public string VisibleBuyTicket
        {
            get => visibleBuyTicket;
            set
            {
                visibleBuyTicket = value;
                OnPropertyChanged("VisibleBuyTicket");
            }
        }
        public string VisibleCreateBus
        {
            get => visibleCreateBus;
            set
            {
                visibleCreateBus = value;
                OnPropertyChanged("VisibleCreateBus");
            }
        }
        public string VisibleSignOut
        {
            get => visibleSignOut;
            set
            {
                visibleSignOut = value;
                OnPropertyChanged("VisibleSignOut");
            }
        }
        public string VisibleSignIn
        {
            get => visibleSignIn;
            set
            {
                visibleSignIn = value;
                OnPropertyChanged("VisibleSignIn");
            }
        }
        public string VisibleSignUp
        {
            get => visibleSignUp;
            set
            {
                visibleSignUp = value;
                OnPropertyChanged("VisibleSignUp");
            }
        }
        public string VisibleReport
        {
            get => visibleReport;
            set
            {
                visibleReport = value;
                OnPropertyChanged("VisibleReport");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
