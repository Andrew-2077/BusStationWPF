using BusStationWPF.Model;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IEditorBus
    {
        void SetCreatorBus(IProcessDoUndo<Bus> process);
        void SetUser(UserModel user);
        event Action BusSaved;
        event Action CancelCurrentProcess;
        IProcessDoUndo<Bus> ProcessBus { get; }
        ICommand GoBack { get; }
        ICommand StartProcess { get; }
    }
}
