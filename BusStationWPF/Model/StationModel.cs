﻿using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class StationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StationModel() { }
        public StationModel(Station item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
        }
    }
}
