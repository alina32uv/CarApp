using CarApp.Pages.Drives;

namespace CarApp.Interfaces
{
    public interface IDrive
    {
        Task<List<Drive>> GetAll();
        Task AddNew(string name);
        Task Update(Drive drive);
        Task Delete(Drive drive);
        Task<Drive> GetById(int id);
    }
}
