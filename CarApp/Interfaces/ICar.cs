using CarApp.Models;
using CarApp.Pages.Car;
using CarApp.Pages.Fuels;
using CarApp.Pages.RentInfo;

namespace CarApp.Interfaces
{
    public interface ICar
    {
        Task<List<Car>> GetAll();
        Task<List<Car>> FilterCars(int fuelId, int transmissionId);
        Task<List<Car>> FilterCars2(IEnumerable<int> vehicleTypes, IEnumerable<int> carBodyTypes,
            IEnumerable<int> Brands, IEnumerable<int> fuelTypes, IEnumerable<int> driveTypes,
            IEnumerable<int> transmissionTypes);
        Task<Filter> GetFilterData();
        Task<List<Car>> Sorting(string sortBy);
        Task AddNew(Car car);
        Task<List<Fuel>> GetFuel();
        Task<Car> GetById(int id);
        Task Update(CarViewModel car);
        Task Delete(CarViewModel car);
        Task RentCar(RentInfo rentModel);
        Task<bool> IsCarAvailable(int carId, DateTime dateBring, DateTime dateReturn);
        Task<bool> Rented(int carId, string userId, DateTime currentDate);
        //public Task<List<Car>> FilterCars(string fuelType, int numberOfDoors);

        // Task Delete(Car car);
    }
}
