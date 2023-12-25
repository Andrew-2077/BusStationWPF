using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using DAL.Entitites;
using BusStationWPF.Model.ModelsForEditingViewStyle;
using BusStationWPF.View;

namespace BusStationWPF.ViewModel
{
    public class EditorBusService : IEditorBus
    {
        private UserModel currentUser;
        public event Action BusSaved;
        public event Action CancelCurrentProcess;
        IProcessDoUndo<Bus> processBus;
        public IProcessDoUndo<Bus> ProcessBus { get => processBus; }
        public void SetCreatorBus(IProcessDoUndo<Bus> process)
        {
            processBus = process;
            processBus.ProcessComplete += SaveBus;
        }
        public void SetUser(UserModel user) => this.currentUser = user;
        void SaveBus(Bus bus)
        {
            bus.IdUserCreator = currentUser.Id;
            db.Bus.Create(bus);
            db.Save();
            BusSaved?.Invoke();
        }
        public ICommand StartProcess
        {
            get => new RelayCommand(obj =>
            {
                processBus.StartProcess(new Bus());
            });
        }
        public ICommand GoBack
        {
            get => new RelayCommand((obj) =>
            {
                processBus.CancelCurrentProcess();
                CancelCurrentProcess?.Invoke();
            });
        }
        public event Action<TypeOfLevelModel> LevelChoosen;
        private IUnitOfWork db;
        public EditorBusService(IUnitOfWork db)
        {
            this.db = db;
        }
        List<TypeOfLevelModel> typeOfLevelModels = null;
        public List<TypeOfLevelModel> TypeOfLevelModels
        {
            get => typeOfLevelModels ?? (typeOfLevelModels = db.TypeOfLevel.GetList().Select(i => new TypeOfLevelModel(i)).ToList());
        }
        ObservableCollection<LevelModel> levels = null;
        public ObservableCollection<LevelModel> Levels
        {
            get => levels ?? (levels = new ObservableCollection<LevelModel>());
        }
        List<StationModel> stationModels = null;
        public List<StationModel> StationModels
        {
            get => stationModels ?? (stationModels = db.Station.GetList().Select(i => new StationModel(i)).ToList());
        }
        ObservableCollection<ModelForEditingSchedule> modelForEditingScheduleCollection = null;
        public ObservableCollection<ModelForEditingSchedule> ModelForEditingScheduleCollection
        {
            get => modelForEditingScheduleCollection ?? (modelForEditingScheduleCollection = new ObservableCollection<ModelForEditingSchedule>());
        }
        ObservableCollection<TimesForStationModel> timesForDeparture = null;
        public ObservableCollection<TimesForStationModel> TimesForDeparture
        {
            get => timesForDeparture ?? (timesForDeparture = new ObservableCollection<TimesForStationModel>());
        }
        private BusModel currentBusModel = null;
        private ButtonInfoBusEditPage buttonInfo = new ButtonInfoBusEditPage();
        public ButtonInfoBusEditPage ButtonInfo { get; }

        public void EditBus(BusModel busModel)
        {
            InfoButtonsOnBusEditPage.Instance.IsEnable = false;
            currentBusModel = busModel;
            (
                from level in db.Level.GetList()
                where level.BusId == busModel.Id
                orderby level.NumberInBus
                select new LevelModel(level)).ToList().ForEach(i => levels.Add(i));
            (from StationBusSchedule in db.ScheduleStationBus.GetList()
             where StationBusSchedule.IdBus == busModel.Id
             orderby StationBusSchedule.Number_flight
             select new StationBusScheduleModel(StationBusSchedule)).ToList().ForEach(i =>
             modelForEditingScheduleCollection.Add(new ModelForEditingSchedule() { StationBusScheduleModel = i }));
            timesForDeparture.Add(LoadDepartureTime(busModel));
            List<KeyValuePair<DateTime, DateTime>> times =
                (from Times in db.TimesForStation.GetList()
                 where Times.RouteId == timesForDeparture.LastOrDefault()?.RouteId
                 orderby Times.DepartureTime
                 select new KeyValuePair<DateTime, DateTime>(Times.ArrivalTime, Times.DepartureTime)).ToList();
            for (int i = 0; i < times.Count; i++)
            {
                modelForEditingScheduleCollection[i].ArrivalTime = times[i].Key;
                modelForEditingScheduleCollection[i].DepartureTime = times[i].Value;
            }
            TimeForFirstStationBusSchedule = timesForDeparture.Last();
            SetDateTimeForFirstStationBusSchedule();
        }
        TimesForStationModel LoadDepartureTime(BusModel busModel)
        {
            return ((from route in db.Route.GetList()
                     join timesForStation in db.TimesForStation.GetList()
                     on route.Id equals timesForStation.RouteId
                     where route.IdBus == busModel.Id
                     orderby timesForStation.DepartureTime
                     group new TimesForStationModel(timesForStation) { loadedInDb = true } by route.Id).FirstOrDefault().FirstOrDefault());
        }
        public void RemoveBus(BusModel busModel)
        {
            Bus bus = busModel.GetBus();
            (from route in db.Route.GetList()
             where route.IdBus != busModel.Id
             join times in db.TimesForStation.GetList()
             on route.Id equals times.RouteId
             orderby times.DepartureTime
             group times by route.Id).ToList()
             .ForEach(i =>
             {
                 if (i.FirstOrDefault().DepartureTime > DateTime.Now)
                 {
                     int idRoute = i.FirstOrDefault().RouteId;
                     i.ToList().ForEach(j => db.TimesForStation.Delete(j.Id));
                     db.Route.Delete(idRoute);
                 }
             });
            if (db.Route.GetList().Where(i => i.IdBus == bus.Id).Count() == 0)
            {
                db.Bus.Delete(bus.Id);
            }
        }
        public void SetDataWhenUserEnterPage(Page page)
        {
            if (page is BusEditPage)
            {
                InfoButtonsOnBusEditPage.Instance.IsEnable = true;
                currentBusModel = new BusModel() { LoadedInDB = false };
                buttonInfo.IsEnabledButtonForAddLevel = buttonInfo.IsEnabledButtonForAddStationInSchedule = true;
            }
        }
        public void SetDataWhenUserLeavePage(Page page)
        {
            if (page is BusEditPage)
            {
                levels.Clear();
                modelForEditingScheduleCollection.Clear();
                timesForDeparture.Clear();
            }
        }
        public ICommand AddStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                timesForDeparture.Add(new TimesForStationModel() { DepartureTime = DateTime.Now, loadedInDb = false });
            });
        }
        public ICommand RemoveStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                if (obj is TimesForStationModel Time)
                {
                    CheckNotTimeForFirstStationBusScheduleAndRemove(Time);
                }
            });
        }
        void CheckNotTimeForFirstStationBusScheduleAndRemove(TimesForStationModel Time)
        {
            if (Time != TimeForFirstStationBusSchedule)
                timesForDeparture.Remove(Time);
            else
            {
                CheckCanRemoveTimeForFirstStationAndRemove();
            }
        }
        void CheckCanRemoveTimeForFirstStationAndRemove()
        {
            if (currentBusModel.LoadedInDB)
            {
                MessageBox.Show("Автобус можно удалить только из вашего профиля!");
                return;
            }
            TimesForStationModel NewTime = timesForDeparture.FirstOrDefault(i => i != TimeForFirstStationBusSchedule);
            if (NewTime != null)
            {
                TimeForFirstStationBusSchedule.DepartureTime = NewTime.DepartureTime;
                timesForDeparture.Remove(NewTime);
            }
            else
                MessageBox.Show("Чтобы удалить это время отправления, создайте другое время отправления или удалите все станции!");
        }
        public ICommand AddLevel
        {
            get => new RelayCommand(obj =>
            {
                levels.Add(new LevelModel() { NumberInBus = ((levels.LastOrDefault()?.NumberInBus) ?? 0) + 1 });
                levels.Last().PropertyChanged += ChangeViewOfLevel;
            });
        }
        void ChangeViewOfLevel(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (changingTime && propertyChangedEventArgs.PropertyName == "TypeOfLevelId")
            {
                LevelChoosen?.Invoke(new TypeOfLevelModel() { Id = (e as LevelModel).TypeOfLevelId });
            }
        }
        public ICommand RemoveLevel
        {
            get => new RelayCommand(obj =>
            {
                if (obj is LevelModel levelModel)
                {
                    levels.Remove(levelModel);
                    for (int i = levelModel.NumberInBus - 1; i < levels.Count; i++)
                        levels[i].NumberInBus--;
                }
            });
        }
        bool ValidateDateBeforeAddBus()
        {
            bool ok = true;
            if (!(ok = levels.Count > 0))
            {
                MessageBox.Show("Добвьте уровни в многоэтажный автобус!");
                return ok;
            }
            if (!(ok = modelForEditingScheduleCollection.Count > 1))
            {
                MessageBox.Show("Добавьте больше станций в расписание остановок автобуса!");
                return ok;
            }
            levels.ToList().ForEach(i =>
            {
                if (typeOfLevelModels.FirstOrDefault(j => j.Id == i.TypeOfLevelId) == null)
                    ok = false;
            });
            if (!ok)
            {
                MessageBox.Show("Заполните все типы уровней, добавленных в автобус!");
                return ok;
            }
            ModelForEditingScheduleCollection.ToList().ForEach(i =>
            {
                if (stationModels.FirstOrDefault(j => j.Id == i.StationBusScheduleModel.IdStation) == null)
                    ok = false;
            });
            if (!ok)
            {
                MessageBox.Show("Заполните все станции, добавленные в расписание автобуса!");
                return ok;
            }
            ModelForEditingScheduleCollection.ToList().ForEach(i =>
            {
                if (ok && ModelForEditingScheduleCollection.Count(j => j.StationBusScheduleModel.IdStation == i.StationBusScheduleModel.IdStation) != 1)
                {
                    MessageBox.Show($"Станция {stationModels.First(j => j.Id == i.StationBusScheduleModel.IdStation).Name} встречается в расписании несколько раз!");
                    ok = false;
                }
                if (ok && i.ArrivalTime < i.PreviousModel.DepartureTime)
                {
                    MessageBox.Show($"Время приезда на станцию {stationModels.First(j => j.Id == i.StationBusScheduleModel.IdStation).Name} меньше, чем на предыдущую станцию!");
                    ok = false;
                }
            });
            return ok;
        }
        public ICommand AddBus
        {
            get => new RelayCommand(obj =>
            {
                if (currentBusModel.LoadedInDB)
                {
                    Bus BusForEdit = db.Bus.GetItem(currentBusModel.Id);
                    for (int i = 1; i < timesForDeparture.Count; i++)
                    {
                        Route route = new Route();
                        BusForEdit.Route.Add(route);
                        TimeSpan dif = timesForDeparture[i].DepartureTime - modelForEditingScheduleCollection[0].DepartureTime;
                        modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                        {
                            route.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime + dif, DepartureTime = modelForEditingSchedule.DepartureTime + dif });
                        });
                    }
                    db.Bus.Update(BusForEdit);
                    db.Save();
                    BusSaved?.Invoke();
                }
                else
                {
                    if (!ValidateDateBeforeAddBus())
                        return;
                    Bus BusForAdd = currentBusModel.GetBus();
                    BusForAdd.IdUserCreator = currentUser.Id;
                    db.Bus.Create(BusForAdd);
                    levels.ToList().ForEach(level => BusForAdd.Level.Add(level.GetLevel()));
                    modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                    {
                        BusForAdd.ScheduleStationBus.Add(modelForEditingSchedule.StationBusScheduleModel.GetStationBusSchedule());
                    });
                    timesForDeparture.ToList().ForEach(dateTimeModel =>
                    {
                        Route route = new Route();
                        BusForAdd.Route.Add(route);
                        TimeSpan dif = dateTimeModel.DepartureTime - modelForEditingScheduleCollection[0].DepartureTime;
                        modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                        {
                            route.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime + dif, DepartureTime = modelForEditingSchedule.DepartureTime + dif });
                        });

                    }
                    );

                    db.Save();
                    BusSaved?.Invoke();
                }
            });


        }
        bool changingTime = true;
        private TimesForStationModel TimeForFirstStationBusSchedule;
        public ICommand AddStationInSchedule
        {
            get => new RelayCommand(obj =>
            {
                modelForEditingScheduleCollection.Add(new ModelForEditingSchedule()
                {
                    PreviousModel = modelForEditingScheduleCollection.LastOrDefault() ?? new ModelForEditingSchedule() { DepartureTime = DateTime.Now },
                    StationBusScheduleModel = new StationBusScheduleModel() { Number_flight = modelForEditingScheduleCollection.Count + 1, IdStation = -1 }
                });
                modelForEditingScheduleCollection.Last().ArrivalTime = modelForEditingScheduleCollection.Last().DepartureTime =
                modelForEditingScheduleCollection.Last().PreviousModel.DepartureTime;
                if (modelForEditingScheduleCollection.Count == 1)
                {
                    TimeForFirstStationBusSchedule = new TimesForStationModel();
                    SetDateTimeForFirstStationBusSchedule();
                    timesForDeparture.Add(TimeForFirstStationBusSchedule);
                    TimeForFirstStationBusSchedule.PropertyChanged += ChangeTimeInStationBusSchedule;
                }
            });
        }
        void SetDateTimeForFirstStationBusSchedule()
        {
            modelForEditingScheduleCollection[0].PropertyChanged += ChangeTimeInStartTime;
            TimeForFirstStationBusSchedule.DepartureTime = modelForEditingScheduleCollection[0].DepartureTime;
        }

        void ChangeTimeInStartTime(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (changingTime && propertyChangedEventArgs.PropertyName == "DepartureTime")
            {
                changingTime = false;
                TimeForFirstStationBusSchedule.DepartureTime = modelForEditingScheduleCollection[0].DepartureTime;
                changingTime = true;
            }
        }
        void ChangeTimeInStationBusSchedule(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (changingTime && propertyChangedEventArgs.PropertyName == "DepartureTime")
            {
                changingTime = false;
                TimeSpan dif = modelForEditingScheduleCollection[0].DepartureTime - TimeForFirstStationBusSchedule.DepartureTime;
                modelForEditingScheduleCollection.ToList().ForEach(i =>
                {
                    i.DepartureTime -= dif;
                    i.ArrivalTime -= dif;
                });
                changingTime = true;
            }
        }
        public ICommand RemoveStationFromSchedule
        {
            get => new RelayCommand(obj =>
            {
                if (obj is ModelForEditingSchedule modelForEditingSchedule)
                {
                    RemoveStationBusSchedule(modelForEditingSchedule);
                }
            });
        }
        void RemoveStationBusSchedule(ModelForEditingSchedule modelForEditingSchedule)
        {
            modelForEditingScheduleCollection.Remove(modelForEditingSchedule);
            for (int i = modelForEditingSchedule.StationBusScheduleModel.Number_flight - 1; i < modelForEditingScheduleCollection.Count; i++)
                modelForEditingScheduleCollection[i].StationBusScheduleModel.Number_flight--;
            if (modelForEditingSchedule.StationBusScheduleModel.Number_flight - 1 >= 0 && modelForEditingSchedule.StationBusScheduleModel.Number_flight - 1 < modelForEditingScheduleCollection.Count)
                modelForEditingScheduleCollection[modelForEditingSchedule.StationBusScheduleModel.Number_flight - 1].PreviousModel = modelForEditingSchedule.PreviousModel;
            CheckRemoveTimeForFirstStation(modelForEditingSchedule);
        }
        void CheckRemoveTimeForFirstStation(ModelForEditingSchedule modelForEditingSchedule)
        {
            if (modelForEditingSchedule.StationBusScheduleModel.Number_flight == 1)
            {
                modelForEditingSchedule.PropertyChanged -= ChangeTimeInStartTime;
                if (modelForEditingScheduleCollection.Count > 0)
                    SetDateTimeForFirstStationBusSchedule();
                else
                    UnSetDateTimeForFirstStationBusSchedule();
            }
        }
        void UnSetDateTimeForFirstStationBusSchedule()
        {
            TimeForFirstStationBusSchedule.PropertyChanged -= ChangeTimeInStationBusSchedule;
            timesForDeparture.Remove(TimeForFirstStationBusSchedule);
        }

    }
}
