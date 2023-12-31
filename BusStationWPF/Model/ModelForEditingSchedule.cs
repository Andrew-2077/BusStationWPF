﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class ModelForEditingSchedule : INotifyPropertyChanged
    {
        DateTime arrivalTime;
        DateTime departureTime;
        ModelForEditingSchedule previousModel;
        StationBusScheduleModel stationBusScheduleModel;

        public DateTime ArrivalTime
        {
            get => arrivalTime;
            set
            {
                arrivalTime = value;
                OnPropertyChanged("ArrivalTime");
            }
        }
        public ModelForEditingSchedule PreviousModel
        {
            get => previousModel;
            set
            {
                previousModel = value;
                OnPropertyChanged("PreviousModel");
            }
        }
        public DateTime DepartureTime
        {
            get => departureTime;
            set
            {
                departureTime = value;
                OnPropertyChanged("DepartureTime");
            }
        }
        public StationBusScheduleModel StationBusScheduleModel
        {
            get => stationBusScheduleModel;
            set
            {
                stationBusScheduleModel = value;
                OnPropertyChanged("StationBusScheduleModel");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
