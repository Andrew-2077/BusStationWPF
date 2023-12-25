using BusStationWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface IReportCompileStrategy
    {
        ReportModel CompileReport(ConcreteWayFromStationToStation way);
    }
}
