using CarRental.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using System.ServiceModel;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using System.Security.Permissions;
using CarRental.Common;
using CarRental.Data.Contracts;
using Core.Common.Exceptions;

namespace CarRental.Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AccountManager : ManagerBase, IAccountService
    {
        public AccountManager()
        {

        }

        public AccountManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            Account authAccount = accountRepository.GetByLogin(loginName);
            if (authAccount == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Cannot find account for login name {0} to use for security trimming.", loginName));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return authAccount;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Account GetCustomerAccountInfo(string loginEmail)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Account with login {0} is not in database.", loginEmail));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(account);

            return account;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public void UpdateCustomerAccountInfo(Account account)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            ValidateAuthorization(account);

            Account updatedAccount = accountRepository.Update(account);
        }
    }
}
