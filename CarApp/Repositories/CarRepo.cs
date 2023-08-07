using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Car;
using CarApp.Pages.Fuels;
using CarApp.Pages.RentInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;

namespace CarApp.Repositories
{
    public class CarRepo : ICar
    {
        private readonly CarAppContext ctx;

        public CarRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(Car car)
        {
            ctx.Car.Add(car);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Car>> GetAll()
        {
            var cars = await ctx.Car
        .Include(c => c.Fuel)
        .Include(c => c.Drive)
        .Include(c => c.CarBodyType)
        .Include(c => c.VehicleType)
        .Include(c => c.Brand)
        .Include(c => c.Transmission)
        .ToListAsync();
            return cars;
        }
        public async Task<Filter> GetFilterData()
        {
            var filter = new Filter();

            filter.Cars = await ctx.Car.ToListAsync();
            filter.CarBodyTypes = await ctx.CarBodyType.ToListAsync();
            filter.VehicleTypes = await ctx.VehicleType.ToListAsync();

            return filter;
        }


        public async Task<List<Car>> FilterCars(int vehicleId, int bodyId)
        {
            var filteredCars = await ctx.Car
                .Where(c => c.VehicleTypeId == vehicleId && c.CarBodyTypeId == bodyId)
                .Include(c => c.Fuel)
                .Include(c => c.Transmission)
                .Include(c => c.CarBodyType)
                .Include(c => c.VehicleType)
                .Include(c => c.Brand)
                .Include(c => c.Drive)
                .ToListAsync();

            return filteredCars;
        }

        private async Task<Car?> GetByIdTest(int id)
        {
            return await ctx.Car.FindAsync(id);
        }
        public async Task<Car> GetById(int id)
        {
            var carFromDb = await ctx.Car
         .Include(c => c.Fuel)
         .Include(c => c.Transmission)
         .Include(c => c.CarBodyType)
         .Include(c => c.VehicleType)
         .Include(c => c.Brand)
         .Include(c => c.Drive)
         .FirstOrDefaultAsync(c => c.CarId == id);

            if (carFromDb != null)
            {
                return carFromDb;
            }
            return null;
        }








        public async Task Update(CarViewModel car)
        {
            var existingCar = await GetByIdTest(car.CarId);

            if (existingCar != null)
            {
                existingCar.Name = car.Name;
                existingCar.TransmissionId = car.TransmissionId;
                existingCar.Seats = car.Seats;
                existingCar.DailyPrice = car.DailyPrice;
                existingCar.Year = car.Year;
                existingCar.ModelName = car.ModelName;
                existingCar.Image = car.Image;
                existingCar.CarBodyTypeId = (int)car.CarBodyTypeId;
                existingCar.VehicleTypeId = car.VehicleTypeId;
                existingCar.BrandId = (int)car.BrandId;
                existingCar.FuelId = (int)car.FuelId;
                existingCar.DriveId = (int)car.DriveId;

                //ctx.Entry(existingCar).State = EntityState.Modified;
                ctx.Car.Update(existingCar);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Car>> Sorting(string sortBy)
        {
            var sortedCars = ctx.Car.AsQueryable();
            switch (sortBy)
            {
                case "Price1":
                    sortedCars = sortedCars.OrderBy(c => c.DailyPrice);
                    break;
                case "Price2":
                    sortedCars = sortedCars.OrderByDescending(c => c.DailyPrice);
                    break;
                case "Year2":
                    sortedCars = sortedCars.OrderBy(c => c.Year);
                    break;
                case "Year1":
                    sortedCars = sortedCars.OrderByDescending(c => c.Year);
                    break;
                default:
                    sortedCars = sortedCars.OrderBy(c => c.Name); 
                    break;
            }

            return await sortedCars.ToListAsync();

        }

            public async Task<List<Car>> FilterCars2(IEnumerable<int> vehicleTypes, IEnumerable<int> carBodyTypes,
            IEnumerable<int> Brands, IEnumerable<int> fuelTypes, IEnumerable<int> driveTypes,
            IEnumerable<int> transmissionTypes)
        {



            var filteredVehicleTypes = vehicleTypes.ToList();
            var filteredCarBodyTypes = carBodyTypes.ToList();
            var filteredBrands = Brands.ToList();
            var filteredFuelTypes = fuelTypes.ToList();
            var filteredDriveTypes = driveTypes.ToList();
            var filteredTransmissionTypes = transmissionTypes.ToList();

            if (!filteredVehicleTypes.Any() || !filteredCarBodyTypes.Any() || !filteredBrands.Any() ||
    !filteredFuelTypes.Any() || !filteredDriveTypes.Any() || !filteredTransmissionTypes.Any())
            {
                // Returnează toate mașinile sau un rezultat gol, în funcție de necesități
                return await ctx.Car.ToListAsync();
            }

            var filteredCars = await ctx.Car
                .Where(c => filteredVehicleTypes.Contains(c.VehicleTypeId)
                && filteredCarBodyTypes.Contains(c.CarBodyTypeId) && filteredBrands.Contains(c.BrandId)
                && filteredFuelTypes.Contains(c.FuelId) && filteredDriveTypes.Contains(c.DriveId) 
                && filteredTransmissionTypes.Contains(c.TransmissionId)
                )
                .Include(c => c.Fuel)
                .Include(c => c.Transmission)
                .Include(c => c.CarBodyType)
                .Include(c => c.VehicleType)
                .Include(c => c.Brand)
                .Include(c => c.Drive)
                .ToListAsync();

            return filteredCars;
        }

        public async Task<List<Fuel>> GetFuel()
        {
            return await ctx.Fuel.ToListAsync();
        }

        public async Task Delete(CarViewModel car)
        {

            var existingCar = await GetByIdTest(car.CarId);

            if (existingCar != null)
            {
                existingCar.Name = car.Name;
                existingCar.TransmissionId = car.TransmissionId;
                existingCar.Seats = car.Seats;
                existingCar.DailyPrice = car.DailyPrice;
                existingCar.Year = car.Year;
                existingCar.ModelName = car.ModelName;
                existingCar.Image = car.Image;
                existingCar.CarBodyTypeId = (int)car.CarBodyTypeId;
                existingCar.VehicleTypeId = car.VehicleTypeId;
                existingCar.BrandId =(int)car.BrandId;
                existingCar.FuelId = (int)car.FuelId;
                existingCar.DriveId = (int)car.DriveId;

                ctx.Car.Remove(existingCar);
                await ctx.SaveChangesAsync();
            }
        }






        /*public async Task RentCar(RentInfo rentInfo)
        {
            var car = await ctx.Car.FirstOrDefaultAsync(c => c.CarId == rentInfo.CarId);
            if (car == null)
            {
                throw new ArgumentException("Car not found");
            }

            if (rentInfo.DateBring >= rentInfo.DateReturn)
            {
                throw new ArgumentException("Invalid pickup and return dates. Pickup date must be before return date.");
            }

            bool isCarAvailable = await IsCarAvailableForRent(car.CarId, rentInfo.DateBring, rentInfo.DateReturn);
            if (!isCarAvailable)
            {
                throw new ArgumentException("The car is not available for rent in the specified period.");
            }

            ctx.RentInfo.Add(rentInfo);
            await ctx.SaveChangesAsync();
        }
        private async Task<bool> IsCarAvailableForRent(int carId, DateTime pickupDate, DateTime returnDate)
        {
              var existingRentInfos = await ctx.RentInfo
                .Where(r => r.CarId == carId)
                .ToListAsync();

            foreach (var rentInfo in existingRentInfos)
            {
                if (pickupDate >= rentInfo.DateBring && pickupDate <= rentInfo.DateReturn)
                {
                    return false; 
                }

                if (returnDate >= rentInfo.DateBring && returnDate <= rentInfo.DateReturn)
                {
                    
                    return false; 
                }

                if (pickupDate <= rentInfo.DateBring && returnDate >= rentInfo.DateReturn)
                {
                    return false; 
                }
            }

            return true;
        }*/
        public async Task RentCar(RentInfo rentModel)
        {
            var rentInfo = new RentInfo
            {
                CarId = rentModel.CarId,
                DateBring = rentModel.DateBring,
                DateReturn = rentModel.DateReturn,
                UserId = rentModel.UserId,
                StatusId= rentModel.StatusId
            };

            ctx.RentInfo.Add(rentInfo);
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> IsCarAvailable(int carId, DateTime dateBring, DateTime dateReturn)
        {
            var existingRents = await ctx.RentInfo
                .Where(r => r.CarId == carId)
                .ToListAsync();

           foreach (var rent in existingRents)
            {
                if (!(rent.DateReturn <= dateBring || rent.DateBring >= dateReturn))
                {
                   
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> Rented(int carId, string userId, DateTime currentDate)
        {
            var existingRents = await ctx.RentInfo
                .Where(r => r.CarId == carId && r.UserId == userId)
                .ToListAsync();

            foreach (var rent in existingRents)
            {
                if (rent.DateReturn >= currentDate)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
