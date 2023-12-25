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
    public class LevelModel : INotifyPropertyChanged
    {
        private int id;
        private int busId;
        private int typeOfLevelId;
        private int numberInBus;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int TypeOfLevelId
        {
            get => typeOfLevelId;
            set
            {
                typeOfLevelId = value;
                OnPropertyChanged("TypeOfLevelId");
            }
        }

        public int BusId
        {
            get => busId;
            set
            {
                busId = value;
                OnPropertyChanged("BusId");
            }
        }

        public int NumberInBus
        {
            get => numberInBus;
            set
            {
                numberInBus = value;
                OnPropertyChanged("NumberInBus");
            }
        }
        public LevelModel() { }
        public LevelModel(Level level)
        {
            this.id = level.Id;
            this.BusId = level.BusId;
            this.numberInBus = level.NumberInBus;
            this.typeOfLevelId = level.TypeOfLevelId;
        }
        public Level GetLevel() => new Level() { Id = this.id, NumberInBus = this.numberInBus, BusId = this.busId, TypeOfLevelId = this.typeOfLevelId };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
