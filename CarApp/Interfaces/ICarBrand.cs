using CarApp.Pages.Brands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarApp.Interfaces
{
    public interface ICarBrand
    {
       Task<List<Brand>> GetAll();
        Task AddNew(string name);
        Task Update(Brand brand);
        Task Delete(Brand brand);
        Task<Brand> GetById( int id);
    }
}
