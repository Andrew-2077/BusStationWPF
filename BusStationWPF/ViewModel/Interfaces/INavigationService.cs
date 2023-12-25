using BusStationWPF.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BusStationWPF.Model;

namespace BusStationWPF.ViewModel.Interfaces
{
    public interface INavigationService
    {
        event Action<Page> Leave;
        event Action<Page> Enter;
        void SetPageFrame(Frame frame);
        void ClearHistoryPage();
        void LoadPreviousPage();
        void LoadNextPage(TypePage typePage);
        void LoadPreviousPageWithNotify();
        void LoadNextPageWithNotify(TypePage typePage);
        void LoadPage(TypePage typePage);
        void LoadPageWithNotify(TypePage typePage);
        void SetViewModel(IViewModel viewModel);
        ICommand GoToPreviousPageWithNotify { get; }
        ICommand GoToPreviousPage { get; }
    }
}
