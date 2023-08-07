using CarApp.Pages.Insurances;

namespace CarApp.Interfaces
{
    public interface IInsurance
    {
        Task<List<Insurance>> GetAll();
        Task AddNew(string name);
        Task Update(Insurance ins);
        Task Delete(Insurance ins);
        Task<Insurance> GetById(int id);
    }
}
