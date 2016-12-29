using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Data_Repositories
{
    [Export(typeof(IRentalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RentalRepository : DataRepositoryBase<Rental>, IRentalRepository
    {
        protected override Rental AddEntity(CarRentalContext entityContext, Rental entity)
        {
            return entityContext.RentalSet.Add(entity);
        }
        protected override Rental UpdateEntity(CarRentalContext entityContext, Rental entity)
        {
            return (from e in entityContext.RentalSet
                    where e.RentalId == entity.RentalId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Rental> GetEntities(CarRentalContext entityContext)
        {
            return (from e in entityContext.RentalSet
                    select e);
        }

        protected override Rental GetEntity(CarRentalContext entityContext, int id)
        {
            return (from e in entityContext.RentalSet
                    where e.RentalId == id
                    select e).FirstOrDefault();
        }
        public IEnumerable<Rental> GetRentalHistoryByAccount(int accountId)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                return (from e in entityContext.RentalSet
                        where e.AccountId == accountId
                        select e).ToFullyLoaded();
            }
        }

        public Rental GetCurrentRentalByCar(int carId)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                return (from e in entityContext.RentalSet
                        where e.CarId == carId && e.DateReturned == null
                        select e).FirstOrDefault();
            }
        }

        public IEnumerable<Rental> GetCurrentlyRentedCars()
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                return (from e in entityContext.RentalSet
                        where e.DateReturned == null
                        select e).ToFullyLoaded();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByCar(int carId)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                return (from e in entityContext.RentalSet
                        where e.CarId == carId
                        select e).ToFullyLoaded();
            }
        }

        //public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
