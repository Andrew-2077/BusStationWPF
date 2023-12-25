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
    public class EditorStartTime : IProcessDoUndo<Bus>
    {
        IUnitOfWork db;
        IProcessDoUndo<Bus> processDoUndo;
        public event Action<Bus> ProcessComplete;
        public static event Action StartProcessLevels;
        ObservableCollection<TimesForStationModel> startTimes = new ObservableCollection<TimesForStationModel>();

        Bus busForEdit;
        bool active = false;
        public EditorStartTime(IUnitOfWork db, IProcessDoUndo<Bus> processDoUndo)
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
                TimesForStation firstTime = busForEdit.Route.First().TimesForStation.First();
                List<TimesForStation> times = busForEdit.Route.First().TimesForStation.ToList().Where(i => i.RouteId == firstTime.RouteId).OrderBy(i => i.DepartureTime).ToList();
                startTimes.ToList().ForEach(i =>
                {
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
