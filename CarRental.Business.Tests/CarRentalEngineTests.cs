using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Business.Entities;
using Moq;
using CarRental.Data.Contracts;
using Core.Common.Contracts;
using CarRental.Business.Common;
using CarRental.Business.Business_Engines;

namespace CarRental.Business.Tests
{
    [TestClass]
    public class CarRentalEngineTests
    {
        [TestMethod]
        public void IsCarCurrentlyRented_any_account()
        {
            Rental rental = new Rental()
            {
                CarId = 1
            };

            Mock<IRentalRepository> mockRentalRepository = new Mock<IRentalRepository>();
            mockRentalRepository.Setup(obj => obj.GetCurrentRentalByCar(1)).Returns(rental);

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(obj => obj.GetDataRepository<IRentalRepository>()).Returns(mockRentalRepository.Object);

            ICarRentalEngine carRentalEngine = new CarRentalEngine(mockDataRepositoryFactory.Object);

            Assert.IsFalse(carRentalEngine.IsCarCurrentlyRented(2));
            Assert.IsTrue(carRentalEngine.IsCarCurrentlyRented(1));
        }
    }
}
