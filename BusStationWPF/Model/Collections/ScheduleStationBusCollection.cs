using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model.Collections
{
    public class ScheduleStationBusCollection : INotifyPropertyChanged
    {
        ObservableCollection<ModelForEditingSchedule> stationSchedule = new ObservableCollection<ModelForEditingSchedule>();
        public ObservableCollection<ModelForEditingSchedule> StationSchedule
        {
            get => stationSchedule;
            set
            {
                stationSchedule = value;
                OnPropertyChanged("StationSchedule");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
