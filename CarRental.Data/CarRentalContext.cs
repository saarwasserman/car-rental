using CarRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext()
            : base("name=CarRental")
        {
            Database.SetInitializer<CarRentalContext>(new CreateDatabaseIfNotExists<CarRentalContext>());
        }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<Car> CarSet { get; set; }
        public DbSet<Rental> RentalSet { get; set; }
        public DbSet<Reservation> ReservationSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove table name's  pluralizing entity name. i.e., 'Account' (entity) -> 'Account' (table name) and not 'Accounts'
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Won't create a table for entity's property with that type
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            // Set Keys of tables
            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Car>().HasKey<int>(e => e.CarId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Rental>().HasKey<int>(e => e.RentalId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Reservation>().HasKey<int>(e => e.ReservationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Car>().Ignore(e => e.CurrentlyRented);
        }
    }
}
