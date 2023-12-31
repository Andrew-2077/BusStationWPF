﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model.ModelsForGetInfoFromView
{
    public class InfoAboutSearchingWaysModel : INotifyPropertyChanged
    {
        int idStartStation;
        int idEndStation;
        DateTime dateTimeArriving = DateTime.Now;
        public int IdStartStation
        {
            get => idStartStation;
            set
            {
                idStartStation = value;
                OnPropertyChanged("IdStartStation");
            }
        }
        public int IdEndStation
        {
            get => idEndStation;
            set
            {
                idEndStation = value;
                OnPropertyChanged("IdEndStation");
            }
        }
        public DateTime DateTimeArriving
        {
            get => dateTimeArriving;
            set
            {
                dateTimeArriving = value;
                OnPropertyChanged("DateTimeArriving");
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
