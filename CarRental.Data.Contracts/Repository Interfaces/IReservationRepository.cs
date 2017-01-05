using CarRental.Business.Entities;
using CarRental.Data.Contracts;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Contracts
{
    public interface IReservationRepository : IDataRepository<Reservation>
    {
        IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo();
        IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId);
        IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate);
    }
}
