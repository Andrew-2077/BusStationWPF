using BusStationWPF.Model;
using BusStationWPF.ViewModel.DecorateTicketService;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel
{
    public class FabricChooseTicket
    {
        private UserModel user;
        private IUnitOfWork db;
        public FabricChooseTicket(UserModel user, IUnitOfWork db)
        {
            this.user = user;
            this.db = db;
        }

        public IChooseTicketService GetChooseTicketService()
        {
            IChooseTicketService first = new ChooseTicketService();
            IChooseTicketService second = new FillPassengersForTicketService(db, user, first);
            return second;
        }
    }
}
