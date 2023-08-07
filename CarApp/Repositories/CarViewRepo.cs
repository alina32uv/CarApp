using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Pages.Brands;
using CarApp.Pages.Car;
using CarApp.Pages.Drives;
using CarApp.Pages.Fuels;

using CarApp.Pages.Fuels;
using CarApp.Pages.Transmissions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarApp.Repositories
{
    public class CarViewRepo : ICarView
    {
        private readonly CarAppContext ctx;

        public CarViewRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(CarViewModel carViewModel)
        {
            var car = new Car
            {

                Name = carViewModel.Name,
                TransmissionId = carViewModel.TransmissionId,
                Seats = carViewModel.Seats,
                DailyPrice = carViewModel.DailyPrice,
                Year=carViewModel.Year,
                ModelName = carViewModel.ModelName,
                Image = carViewModel.Image,
                CarBodyTypeId = (int)carViewModel.CarBodyTypeId,
                VehicleTypeId = carViewModel.VehicleTypeId,
                BrandId = (int)carViewModel.BrandId,
                FuelId = (int)carViewModel.FuelId,
                DriveId = (int)carViewModel.DriveId,
                
            };

            ctx.Car.Add(car);

            await ctx.SaveChangesAsync();


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
                .Where(c => filteredVehicleTypes.Contains(c.VehicleTypeId))
                .Where(c => filteredCarBodyTypes.Contains(c.CarBodyTypeId))
                .Where(c => filteredBrands.Contains(c.BrandId))
                .Where(c => filteredFuelTypes.Contains(c.FuelId))
                .Where(c => filteredDriveTypes.Contains(c.DriveId))
                .Where(c => filteredTransmissionTypes.Contains(c.TransmissionId))
                .Include(c => c.Fuel)
                .Include(c => c.Transmission)
                .Include(c => c.CarBodyType)
                .Include(c => c.VehicleType)
                .Include(c => c.Brand)
                .Include(c => c.Drive)
                .ToListAsync();

            return filteredCars;
        }


        public async Task<List<Entities.CarBodyType>> GetCarBody()
        {
            return ctx.CarBodyType.ToList();
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


        public async Task Update(CarViewModel carViewModel)
        {
            var existingCar = await GetById(carViewModel.CarId);

            if (existingCar != null)
            {
                existingCar.Name = carViewModel.Name;
                existingCar.TransmissionId = carViewModel.TransmissionId;
                existingCar.Seats = carViewModel.Seats;
                existingCar.DailyPrice = carViewModel.DailyPrice;
                existingCar.Year = carViewModel.Year;
                existingCar.ModelName = carViewModel.ModelName;
                existingCar.Image = carViewModel.Image;
                existingCar.CarBodyTypeId = (int)carViewModel.CarBodyTypeId;
                existingCar.VehicleTypeId = carViewModel.VehicleTypeId;
                existingCar.BrandId = (int)carViewModel.BrandId;
                existingCar.FuelId = (int)carViewModel.FuelId;
                existingCar.DriveId = (int)carViewModel.DriveId;

                ctx.Entry(existingCar).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }



        public async Task Delete(CarViewModel carViewModel)
        {
            var existingCar = await GetById(carViewModel.CarId);

            if (existingCar != null)
            {
                existingCar.Name = carViewModel.Name;
                existingCar.TransmissionId = carViewModel.TransmissionId;
                existingCar.Seats = carViewModel.Seats;
                existingCar.DailyPrice = carViewModel.DailyPrice;
                existingCar.Year = carViewModel.Year;
                existingCar.ModelName = carViewModel.ModelName;
                existingCar.Image = carViewModel.Image;
                existingCar.CarBodyTypeId = (int)carViewModel.CarBodyTypeId;
                existingCar.VehicleTypeId = carViewModel.VehicleTypeId;
                existingCar.BrandId = (int)carViewModel.BrandId;
                existingCar.FuelId = (int)carViewModel.FuelId;
                existingCar.DriveId = (int)carViewModel.DriveId;

                //ctx.Entry(existingCar).State = EntityState.Modified;
                ctx.Car.Remove(existingCar);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<CarViewModel>> GetAll()
        {
            var cars = await ctx.CarViewModel
       .Include(c => c.Fuels)
       .Include(c => c.Transmissions)
       .Include(c => c.carBodyTypes)
       .Include(c => c.vehicleTypes)
       .Include(c => c.Brands)
       .Include(c => c.Drives)
       .ToListAsync();

            return cars;
        }
        public async Task<List<CarViewModel>> FilterCars(int vehicleId, int bodyId)
        {
            var filteredCars = await ctx.CarViewModel
                .Where(c => c.VehicleTypeId == vehicleId && c.CarBodyTypeId == bodyId)
                .Include(c => c.Fuels)
                .Include(c => c.Transmissions)
                .Include(c => c.carBodyTypes)
                .Include(c => c.vehicleTypes)
                .Include(c => c.Brands)
                .Include(c => c.Drives)
                .ToListAsync();

            return filteredCars;
        }





        


        public async Task<List<Brand>> GetBrand()
        {
            return ctx.Brand.ToList();
        }

        public async Task<List<Drive>> GetDrive()
        {
            return ctx.Drive.ToList();
        }

        public async Task<List<Fuel>> GetFuel()
        {
            return ctx.Fuel.ToList();
        }

        public async Task<List<Pages.Vehicle.VehicleType>> GetVehicleType()
        {
            return ctx.VehicleType.ToList();
        }

        public async Task<List<Transmission>> GetTransmission()
        {
            return ctx.Transmission.ToList();
        }

       
    }
}
