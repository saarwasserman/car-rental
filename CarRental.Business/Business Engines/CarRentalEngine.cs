using CarRental.Business.Common;
using CarRental.Business.Entities;
using CarRental.Common;
using CarRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
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
        [ImportingConstructor]
        public CarRentalEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        } 

        IDataRepositoryFactory _DataRepositoryFactory;

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

        public bool IsCarCurrentlyRented(int carId)
        {
            bool rented = false;

            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null)
                rented = true;

            return rented;
        }

        public bool IsCarCurrentlyRented(int carId, int accountId)
        {
            bool rented = false;

            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null && currentRental.AccountId == accountId)
                rented = true;

            return rented;
        }

        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
        {
            if (rentalDate > DateTime.Now)
                throw new UnableToRentForDateException(string.Format("Cannot rent for specified date '{0}'.", rentalDate));

            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            bool carIsRented = IsCarCurrentlyRented(carId);
            if (carIsRented)
            {
                throw new CarCurrentlyRentedException(string.Format("Car {0} is already rented"));
            }

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
            {
                throw new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));
            }

            Rental rental = new Rental()
            {
                AccountId = account.AccountId,
                CarId = carId,
                DateRented = rentalDate,
                DateDue = returnDate
            };

            Rental savedEntity = rentalRepository.Add(rental);

            return savedEntity;
        }
    }
}
