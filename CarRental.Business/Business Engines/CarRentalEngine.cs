using CarRental.Business.Common;
using CarRental.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRental.Business.Business_Engines
{
    [Export(typeof(ICarRentalEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRentalEngine : ICarRentalEngine
    {
        public bool IsCarAvailable(int carId, DateTime pickupDate,
                                   DateTime returnDate, IEnumerable<Rental> rentedCars,
                                   IEnumerable<Reservation> reservedCars)
        {
            bool available = true;

            Reservation reservation = reservedCars.Where(item => item.CarId == carId).FirstOrDefault();
            if (reservation != null && (
                (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
                (returnDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)))
            {
                available = false;
            }

            if (available)
            {
                Rental rentel = rentedCars.Where(item => item.CarId == carId).FirstOrDefault();
                if (rentel != null && returnDate < rentel.DateDue)
                {
                    available = false;
                }
            }

            return available;
        }
    }
}
