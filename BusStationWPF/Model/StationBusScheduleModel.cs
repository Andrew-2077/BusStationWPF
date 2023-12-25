using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class StationBusScheduleModel : INotifyPropertyChanged
    {
        private int id;
        private int idStation;
        private int idBus;
        private int number_flight;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int IdStation
        {
            get => idStation;
            set
            {
                idStation = value;
                OnPropertyChanged("IdStation");
            }
        }

        public int IdBus
        {
            get => idBus;
            set
            {
                idBus = value;
                OnPropertyChanged("IdBus");
            }
        }

        public int Number_flight
        {
            get => number_flight;
            set
            {
                number_flight = value;
                OnPropertyChanged("Number_flight");
            }
        }
        public StationBusScheduleModel(StationBusSchedule item)
        {
            this.id = item.Id;
            this.idStation = item.IdStation;
            this.idBus = item.IdBus;
            this.number_flight = item.Number_flight;
        }
        public StationBusScheduleModel() { }
        public StationBusSchedule GetStationBusSchedule() => new StationBusSchedule() { Id = this.Id, IdBus = this.idBus, IdStation = this.idStation, Number_flight = this.number_flight };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
