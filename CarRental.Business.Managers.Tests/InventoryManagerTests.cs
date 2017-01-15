using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using Moq;
using CarRental.Data.Contracts;
using CarRental.Business.Managers;
using System.Security.Principal;
using System.Threading;

namespace CarRental.Business.Managers.Tests
{
    [TestClass]
    public class InventoryManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            GenericPrincipal principal = new GenericPrincipal(
                new GenericIdentity("Saar"), new string[] { "CarRentalAdmin" });

            Thread.CurrentPrincipal = principal;
        }

        [TestMethod]
        public void UpdateCar_add_new()
        {
            Car newCar = new Car();
            Car addedCar = new Car() { CarId = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(obj => obj.GetDataRepository<ICarRepository>().Add(newCar)).Returns(addedCar);

            InventoryManager inventoryManager = new InventoryManager(mockDataRepositoryFactory.Object);

            Car results = inventoryManager.UpdateCar(newCar);

            Assert.IsTrue(results == addedCar);
        }

        [TestMethod]
        public void UpdateCar_update_existing()
        {
            Car existingCar = new Car() { CarId = 1 };
            Car updatedCar = new Car() { CarId = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(obj => obj.GetDataRepository<ICarRepository>().Update(existingCar)).Returns(updatedCar);

            InventoryManager inventoryManager = new InventoryManager(mockDataRepositoryFactory.Object);

            Car results = inventoryManager.UpdateCar(existingCar);

            Assert.IsTrue(results == updatedCar);
        }
    }
}
