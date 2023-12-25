using BusStationWPF.Model;
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
using BusStationWPF.Model.StaticModelsForPassInfo;

namespace BusStationWPF.ViewModel.EditorsBusDecorators
{
    public class EditorLevel : IProcessDoUndo<Bus>
    {
        IUnitOfWork db;
        IProcessDoUndo<Bus> processDoUndo;
        public event Action<Bus> ProcessComplete;
        public static event Action StartProcessLevels;
        public static event Action<TypeOfLevelModel> SelectedLevelChanged;
        ObservableCollection<LevelModel> levels = new ObservableCollection<LevelModel>();

        List<TypeOfLevelModel> typeOfLevelModels;
        Bus busForEdit;
        bool active = false;
        public EditorLevel(IUnitOfWork db, IProcessDoUndo<Bus> processDoUndo)
        {
            this.db = db;
            //this.busForEdit = busForEdit;
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
            levels = new ObservableCollection<LevelModel>();
            typeOfLevelModels = db.TypeOfLevel.GetList().Select(i => new TypeOfLevelModel(i)).ToList();
            active = true;
        }
        void ClearData()
        {
            levels.Clear();
            typeOfLevelModels.Clear();
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
                return !active && processDoUndo != null ? processDoUndo.InfoForView : (new InfoForPassToEditLevelPage()
                {
                    TypeOflevelModels = typeOfLevelModels,
                    LevelModels = levels
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
                LevelModel levelModelForAdd = new LevelModel() { NumberInBus = levels.Count != 0 ? levels[levels.Count - 1].NumberInBus + 1 : 1 };
                levels.Add(levelModelForAdd);
                levelModelForAdd.PropertyChanged += ChangeViewOfLevel;
            });
        }
        void ChangeViewOfLevel(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "TypeOfLevelId")
            {
                SelectedLevelChanged?.Invoke(new TypeOfLevelModel() { Id = (e as LevelModel).TypeOfLevelId });
            }
        }
        public ICommand UndoProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.UndoProcess : new RelayCommand((obj) =>
            {
                if (obj is LevelModel levelForRemove)
                {
                    levels.Remove(levelForRemove);
                    for (int i = levelForRemove.NumberInBus - 1; i < levels.Count; i++)
                        levels[i].NumberInBus--;
                }
            });
        }
        public ICommand СompleteProcess
        {
            get => !active && processDoUndo != null ? processDoUndo.СompleteProcess : new RelayCommand((obj) =>
            {
                levels.ToList().ForEach(i => busForEdit.Level.Add(i.GetLevel()));
                ProcessComplete?.Invoke(busForEdit);
            }, (obj) =>
            {
                if (levels.Count <= 0 || levels.ToList().Any(i => typeOfLevelModels.FirstOrDefault(j => j.Id == i.TypeOfLevelId) == null))
                    return false;
                return true;
            });
        }
    }
}
