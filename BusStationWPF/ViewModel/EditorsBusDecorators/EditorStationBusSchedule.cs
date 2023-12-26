using BusStationWPF.Model;
using BusStationWPF.Model.StaticModelsForPassInfo;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.EditorsBusDecorators
{
    public class EditorStationBusSchedule : IProcessDoUndo<Bus>
    {
        IUnitOfWork db;
        IProcessDoUndo<Bus> processDoUndo;
        public event Action<Bus> ProcessComplete;
        public static event Action StartProcessLevels;
        ObservableCollection<ModelForEditingSchedule> schedules = new ObservableCollection<ModelForEditingSchedule>();

        List<StationModel> stationModels;
        Bus busForEdit;
        bool active = false;
        public EditorStationBusSchedule(IUnitOfWork db, IProcessDoUndo<Bus> processDoUndo)
        {
            this.db = db;
            SetProcesserDoUndo(processDoUndo);
        }
        void SetProcesserDoUndo(IProcessDoUndo<Bus> processDoUndo)
        {
            if (processDoUndo != null)
            {
                this.processDoUndo = processDoUndo;
                this.processDoUndo.ProcessComplete += SetStartDataAndStartProcess;
            }
        }
        void SetStartDataAndStartProcess(Bus busForEdit)
        {
            SetStartData(busForEdit);
            StartProcessLevels?.Invoke();
        }
        void SetStartData(Bus busForEdit)
        {
            this.busForEdit = busForEdit;
            schedules = new ObservableCollection<ModelForEditingSchedule>();
            stationModels = db.Station.GetList().Select(i => new StationModel(i)).ToList();
            active = true;
        }
        void ClearData()
        {
            schedules.Clear();
            stationModels.Clear();
            busForEdit = null;
        }
        public void StartProcess(Bus bus)
        {
            if (processDoUndo != null)
            {
                active = false;
                processDoUndo.StartProcess(bus);
            }
            else
                SetStartDataAndStartProcess(bus);
        }
        public object InfoForView
        {
            get
            {
                return !active && processDoUndo != null ? processDoUndo.InfoForView : (new InfoForPassToEditStationBusSchedulePage()
                {
                    StationModels = stationModels,
                    collection = schedules
                });
            }
        }
        public void CancelCurrentProcess()
        {
            if (active)
            {
                active = false;
                ClearData();
            }
            else if (processDoUndo != null)
                processDoUndo.CancelCurrentProcess();
        }
        public ICommand DoProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.DoProcess : new RelayCommand((obj) =>
            {
                ModelForEditingSchedule levelModelForAdd = new ModelForEditingSchedule()
                {
                    ArrivalTime = (schedules.Count == 0 ? DateTime.Now : schedules.Last().DepartureTime) + new TimeSpan(0, 2, 0),
                    DepartureTime = (schedules.Count == 0 ? DateTime.Now : schedules.Last().DepartureTime) + new TimeSpan(0, 2, 0),
                    StationBusScheduleModel = new StationBusScheduleModel()
                    {
                        Number_flight = schedules.Count + 1
                    },
                    PreviousModel = schedules.Count != 0 ? schedules.Last() : new ModelForEditingSchedule() { DepartureTime = DateTime.Now },
                };
                levelModelForAdd.PropertyChanged += ChangeTimeInSchedule;
                schedules.Add(levelModelForAdd);
            });
        }
        void ChangeTimeInSchedule(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ModelForEditingSchedule model = e as ModelForEditingSchedule;
            if (propertyChangedEventArgs.PropertyName == "ArrivalTime")
            {
                if (model.ArrivalTime > model.DepartureTime)
                    model.DepartureTime = model.ArrivalTime;
            }
            else if (propertyChangedEventArgs.PropertyName == "DepartureTime")
            {
                if (schedules.Count > model.StationBusScheduleModel.Number_flight && schedules[model.StationBusScheduleModel.Number_flight].ArrivalTime <= model.DepartureTime)
                    schedules[model.StationBusScheduleModel.Number_flight].ArrivalTime = model.DepartureTime + new TimeSpan(0, 2, 0);
            }
        }
        public ICommand UndoProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.UndoProcess : new RelayCommand((obj) =>
            {
                if (obj is ModelForEditingSchedule model)
                {
                    schedules.Remove(model);
                    if (schedules.Count >= model.StationBusScheduleModel.Number_flight)
                        schedules[model.StationBusScheduleModel.Number_flight - 1].PreviousModel = model.PreviousModel;
                    for (int i = model.StationBusScheduleModel.Number_flight - 1; i < schedules.Count; i++)
                        schedules[i].StationBusScheduleModel.Number_flight--;
                }
            });
        }
        public ICommand СompleteProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.СompleteProcess : new RelayCommand((obj) =>
            {
                Route addedRoute = new Route();
                busForEdit.Route.Add(addedRoute);
                schedules.ToList().ForEach(i =>
                {
                    busForEdit.StationBusSchedule.Add(i.StationBusScheduleModel.GetStationBusSchedule());
                    addedRoute.TimesForStation.Add(new TimesForStation()
                    {
                        ArrivalTime = i.ArrivalTime,
                        DepartureTime = i.DepartureTime
                    });
                });
                ProcessComplete?.Invoke(busForEdit);
            }, (obj) =>
            {
                if (schedules.Count <= 1 || schedules.ToList().Any(i => stationModels.FirstOrDefault(j => j.Id == i.StationBusScheduleModel.IdStation) == null
                || i.ArrivalTime <= i.PreviousModel.DepartureTime) ||
                schedules.Any(i => schedules.Where(j => j.StationBusScheduleModel.IdStation == i.StationBusScheduleModel.IdStation).Count() > 1) ||
                schedules.Any(i => i.ArrivalTime > i.DepartureTime))
                    return false;
                return true;
            });
        }
    }
}
