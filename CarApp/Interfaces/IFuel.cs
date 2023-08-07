using CarApp.Pages.Fuels;

namespace CarApp.Interfaces
{
    public interface IFuel
    {
        Task<List<Fuel>> GetAll();
        Task AddNew(string name);
        Task Update(Fuel fuel);
        Task Delete(Fuel fuel);
        Task<Fuel> GetById(int id);
    }
}
