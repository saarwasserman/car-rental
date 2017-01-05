using CarRental.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Contracts
{
    public class CustomerReservationInfo
    {
        public Account Account { get; set;}
        public Car Car { get; set; }
        public Reservation Reservation { get; set; }
    }
}
