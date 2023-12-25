using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class ConcreteWayBusModel
    {
        public StationModel StartStationModel { get; set; }
        public StationModel EndStationModel { get; set; }
        public StationBusScheduleModel StartStationBusScheduleModel { get; set; }
        public TimesForStationModel StartTimesForStationModel { get; set; }
        public StationBusScheduleModel EndStationBusScheduleModel { get; set; }
        public TimesForStationModel EndTimesForStationModel { get; set; }
        public ConcreteWayBusModel() { }
        public ConcreteWayBusModel(StationBusScheduleModel startStationBusScheduleModel,
            TimesForStationModel startTimesForStationModel,
            StationBusScheduleModel endStationBusScheduleModel,
            TimesForStationModel endTimesForStationModel,
            StationModel startStationModel, StationModel endStationModel)
        {
            StartStationBusScheduleModel = startStationBusScheduleModel;
            StartTimesForStationModel = startTimesForStationModel;
            EndStationBusScheduleModel = endStationBusScheduleModel;
            EndTimesForStationModel = endTimesForStationModel;
            StartStationModel = startStationModel;
            EndStationModel = endStationModel;
        }
    }
}
