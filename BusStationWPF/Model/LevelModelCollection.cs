using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class LevelModelCollection : INotifyPropertyChanged
    {
        public LevelModelCollection()
        {
            levelCollection = new ObservableCollection<LevelModel>();
        }
        private ObservableCollection<LevelModel> levelCollection;
        public ObservableCollection<LevelModel> LevelCollection
        {
            get => levelCollection;
            set
            {
                levelCollection = value;
                OnPropertyChanged("LevelCollection");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
