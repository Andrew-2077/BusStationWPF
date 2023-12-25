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
    public class TypeOfLevelModel : INotifyPropertyChanged
    {
        public string Name
        {
            get;
        }
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public TypeOfLevelModel(TypeOfLevel typeOfLevel)
        {
            Name = typeOfLevel.Name;
            Id = typeOfLevel.Id;
        }
        public TypeOfLevelModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
