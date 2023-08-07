using CarApp.Migrations;
using CarApp.Pages.Transmissions;

namespace CarApp.Interfaces
{
    public interface ITransmission
    {
        Task<List<Transmission>> GetAll();
        Task AddNew(string type);
        Task Update(Transmission transmission);
        Task Delete(Transmission transmission);
        Task<Transmission> GetById(int id);
    }
}
