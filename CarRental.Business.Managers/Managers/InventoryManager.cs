using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using CarRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InventoryManager : ManagerBase, IInventoryService
    {
        public InventoryManager()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
        {
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        public Entities.Car GetCar(int carId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

                Car carEntity = carRepository.Get(carId);
                if (carEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in the database", carId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return carEntity;
            });
        }

        public Entities.Car[] GetAllCars()
        {
            try
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

                IEnumerable<Car> cars = carRepository.Get();
                IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();

                foreach (Car car in cars)
                {
                    Rental rentedCar = rentedCars.Where(item => item.CarId == car.CarId).FirstOrDefault();
                    car.CurrentlyRented = (rentedCar != null);
                }

                return cars.ToArray();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
