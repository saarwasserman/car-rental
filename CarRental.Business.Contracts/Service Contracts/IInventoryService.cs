using CarRental.Business.Entities;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Contracts
{
    [ServiceContract]
    public interface IInventoryService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car GetCar(int carId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car[] GetAllCars();
    }
}
