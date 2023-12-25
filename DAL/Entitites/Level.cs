namespace DAL.Entitites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Level")]
    public partial class Level
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int TypeOfLevelId { get; set; }

        public int BusId { get; set; }

        public int NumberInBus { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual TypeOfLevel TypeOfLevel { get; set; }
    }
}
