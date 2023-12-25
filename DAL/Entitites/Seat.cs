namespace DAL.Entitites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seat")]
    public partial class Seat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seat()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public int NumberOfSeatInLevel { get; set; }

        public int CostPerStation { get; set; }

        public int TypeOfLevelId { get; set; }

        public virtual TypeOfLevel TypeOfLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
