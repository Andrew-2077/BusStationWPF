using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IReportService
    {
        event Action ReportReady;
        event Action ShowReportEnded;
        ICommand SetStrategy { get; }
        ReportModel Report { get; }
        ICommand MakeReport { get; }
        ICommand GoBack { get; }
    }
}
