namespace DAL.Entitites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int Id { get; set; }

        public int Cost { get; set; }

        public int IdSeat { get; set; }

        public int IdPassenger { get; set; }

        public int IdTimesForStationSource { get; set; }

        public int IdTimesForStationDestiny { get; set; }

        public int? UserId { get; set; }

        public virtual Passenger Passenger { get; set; }

        public virtual Seat Seat { get; set; }

        public virtual TimesForStation TimesForStation { get; set; }

        public virtual TimesForStation TimesForStation1 { get; set; }

        public virtual User User { get; set; }
    }
}
