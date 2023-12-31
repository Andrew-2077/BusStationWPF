﻿using BusStationWPF.Model;
using BusStationWPF.ViewModel.Interfaces;
using DAL.Entitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.ViewModel
{
    public class GetCollectionsService : IGetCollectionsService
    {
        UserModel currentUser;
        IUnitOfWork db;
        public GetCollectionsService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetUser(UserModel user)
        {
            currentUser = user;
        }
        public List<Station> Stations
        {
            get => db.Station.GetList();
        }
        public List<StationModel> StationModels
        {
            get => db.Station.GetList().Select(i => new StationModel(i)).ToList();
        }
        public List<PassengerViewModel> PassengerInProfile
        {
            get => db.Passengers.GetList().Where(i => i.UserId == currentUser.Id).Select(i => new PassengerViewModel(i, true)).ToList();
        }
    }
}
