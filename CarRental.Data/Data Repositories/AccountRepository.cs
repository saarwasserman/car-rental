using CarRental.Business.Entities;
using CarRental.Data.Contracts;
using Core.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRental.Data.Data_Repositories
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        protected override Account AddEntity(CarRentalContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(CarRentalContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                    where e.AccountId == entity.AccountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(CarRentalContext entityContext)
        {
            return (from e in entityContext.AccountSet
                    select e);
        }

        protected override Account GetEntity(CarRentalContext entityContext, int id)
        {
            return (from e in entityContext.AccountSet
                    where e.AccountId == id
                    select e).FirstOrDefault();
        }

        public Account GetByLogin(string login)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                return (from e in entityContext.AccountSet
                        where e.LoginEmail == login
                        select e).FirstOrDefault();
            }
        }
    }
}
