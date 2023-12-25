using BusStationWPF.Model.StaticModelsForPassInfo;
using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Entitites;

namespace BusStationWPF.ViewModel.EditorsBusDecorators
{
    public class EditorStartTimeExistBus : IProcessDoUndo<Bus>
    {
        IUnitOfWork db;
        IProcessDoUndo<Bus> processDoUndo;
        public event Action<Bus> ProcessComplete;
        public static event Action StartProcessLevels;
        ObservableCollection<TimesForStationModel> startTimes = new ObservableCollection<TimesForStationModel>();


        Bus busForEdit;
        bool active = false;
        public EditorStartTimeExistBus(IUnitOfWork db, IProcessDoUndo<Bus> processDoUndo)
        {
            this.db = db;
            SetProcessDoUndo(processDoUndo);
        }
        void SetProcessDoUndo(IProcessDoUndo<Bus> processDoUndo)
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
            startTimes = new ObservableCollection<TimesForStationModel>();
            (from routes in busForEdit.Route
             join times in db.TimesForStation.GetList()
             on routes.Id equals times.RouteId
             group times by routes.Id into ways
             from tickets in db.Ticket.GetList()
             where ways.FirstOrDefault(i => i.Id == tickets.IdTimesForStationSource) == null
             select ways).ToList().ForEach(i =>
             {
                 TimesForStation time = i.Where(j => j.DepartureTime == i.Min(k => k.DepartureTime)).First();
                 startTimes.Add(new TimesForStationModel(time) { loadedInDb = true });
             });
            active = true;
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
        void ClearData()
        {
            startTimes.Clear();
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
                return !active && processDoUndo != null ? processDoUndo.InfoForView : (new InfoForPassToEditStartTimesPage()
                {
                    timesForStationModels = startTimes
                });
            }
        }
        public ICommand DoProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.DoProcess : new RelayCommand((obj) =>
            {
                startTimes.Add(new TimesForStationModel() { ArrivalTime = DateTime.Now, DepartureTime = DateTime.Now });
            });
        }
        public ICommand UndoProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.UndoProcess : new RelayCommand((obj) =>
            {
                if (obj is TimesForStationModel model)
                {
                    startTimes.Remove(model);
                }
            });
        }
        public ICommand СompleteProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.СompleteProcess : new RelayCommand((obj) =>
            {
                List<Route> routesForRemove = new List<Route>();
                busForEdit.Route.ToList().ForEach(i =>
                {
                    if (startTimes.FirstOrDefault(j => j.loadedInDb && j.RouteId == i.Id) == null)
                        routesForRemove.Add(i);
                });
                routesForRemove.ForEach(i => busForEdit.Route.Remove(i));
                TimesForStation firstTime = busForEdit.Route.First().TimesForStation.First();
                List<TimesForStation> times = busForEdit.Route.First().TimesForStation.ToList().Where(i => i.RouteId == firstTime.RouteId).OrderBy(i => i.DepartureTime).ToList();
                startTimes.ToList().ForEach(i =>
                {
                    if (i.loadedInDb)
                        return;
                    Route addedRoute = new Route();
                    busForEdit.Route.Add(addedRoute);
                    var sub = times[0].DepartureTime - i.DepartureTime;
                    for (int j = 0; j < times.Count; j++)
                        addedRoute.TimesForStation.Add(new TimesForStation()
                        {
                            ArrivalTime = times[j].ArrivalTime - sub,
                            DepartureTime = times[j].DepartureTime - sub
                        });
                });
                ProcessComplete?.Invoke(busForEdit);
            });
        }
    }
}
