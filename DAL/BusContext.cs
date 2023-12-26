using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entitites
{
    public partial class BusContext : DbContext
    {
        public BusContext()
            : base("name=BusContext")
        {
        }

        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<StationBusSchedule> StationBusSchedule { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TimesForStation> TimesForStation { get; set; }
        public virtual DbSet<TypeOfLevel> TypeOfLevel { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<CellStructureLevel> CellStructureLevel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Level)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Route)
                .WithRequired(e => e.Bus)
                .HasForeignKey(e => e.IdBus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bus>()
                .HasMany(e => e.StationBusSchedule)
                .WithRequired(e => e.Bus)
                .HasForeignKey(e => e.IdBus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Passenger)
                .HasForeignKey(e => e.IdPassenger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.TimesForStation)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seat>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Seat)
                .HasForeignKey(e => e.IdSeat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Station>()
                .HasMany(e => e.StationBusSchedule)
                .WithRequired(e => e.Station)
                .HasForeignKey(e => e.IdStation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimesForStation>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.TimesForStation)
                .HasForeignKey(e => e.IdTimesForStationSource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimesForStation>()
                .HasMany(e => e.Ticket1)
                .WithRequired(e => e.TimesForStation1)
                .HasForeignKey(e => e.IdTimesForStationDestiny)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfLevel>()
                .HasMany(e => e.Level)
                .WithRequired(e => e.TypeOfLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfLevel>()
                .HasMany(e => e.Seat)
                .WithRequired(e => e.TypeOfLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfLevel>()
                .HasMany(e => e.CellStructureLevel)
                .WithRequired(e => e.TypeOfLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bus)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUserCreator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Passenger)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.TypeOfUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
