namespace DAL.Entitites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduleStationBus
    {
        public int Id { get; set; }

        public int IdStation { get; set; }

        public int IdBus { get; set; }

        public int Number_flight { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual Station Station { get; set; }
    }
}
