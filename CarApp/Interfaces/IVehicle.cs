using CarApp.Pages.Vehicle;

namespace CarApp.Interfaces
{
    public interface IVehicle
    {
        Task<List<VehicleType>> GetAll();
        Task AddNew(string name);
        Task Update(VehicleType vehicle);
        Task Delete(VehicleType vehicle);
        Task<VehicleType> GetById(int id);
    }
}
