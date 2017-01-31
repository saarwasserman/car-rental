﻿using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {
        public void DeleteCar(int carId)
        {
            Channel.DeleteCar(carId);
        }

        public Task DeleteCarAsync(int carId)
        {
            return Channel.DeleteCarAsync(carId);
        }

        public Car[] GetAllCars()
        {
            return Channel.GetAllCars();
        }

        public Task<Car[]> GetAllCarsAsync()
        {
            return Channel.GetAllCarsAsync();
        }

        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCars(pickupDate, returnDate);
        }

        public Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCarsAsync(pickupDate, returnDate);
        }

        public Car GetCar(int carId)
        {
            return Channel.GetCar(carId);
        }

        public Task<Car> GetCarAsync(int carId)
        {
            return Channel.GetCarAsync(carId);
        }

        public Car UpdateCar(Client.Entities.Car car)
        {
            return Channel.UpdateCar(car);
        }

        public Task<Car> UpdateCarAsync(Client.Entities.Car car)
        {
            return Channel.UpdateCarAsync(car);
        }
    }
}
