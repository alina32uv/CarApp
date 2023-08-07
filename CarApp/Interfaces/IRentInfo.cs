using CarApp.Models;
using CarApp.Pages.RentInfo;
using CarApp.Pages.Status;

namespace CarApp.Interfaces
{
    public interface IRentInfo
    {
        Task<List<RentInfo>> GetAll();
        Task Update(RentInfo rent);
        //Task UpdateModel(RentModel rent);
       
        Task Delete(RentInfo rent);
        Task<RentInfo> GetById(int id);
        Task<List<Status>> GetStatus();
       
    }
}
