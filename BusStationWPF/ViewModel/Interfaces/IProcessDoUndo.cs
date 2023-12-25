using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IProcessDoUndo <T>
    {
        object InfoForView { get; }
        event Action<T> ProcessComplete;
        void StartProcess(T obj);
        void CancelCurrentProcess();
        ICommand DoProcess { get; }
        ICommand UndoProcess { get; }
        ICommand СompleteProcess { get; }
    }
}
