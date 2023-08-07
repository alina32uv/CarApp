using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Pages.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class VehicleRepo : IVehicle
    {
        private readonly CarAppContext ctx;

        public VehicleRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string name)
        {
            await ctx.VehicleType.AddAsync(new Pages.Vehicle.VehicleType { Name = name });
            await ctx.SaveChangesAsync();
        }

        public async Task Delete(Pages.Vehicle.VehicleType vehicle)
        {
            var existingtype = await GetById(vehicle.VehicleId);
            if (existingtype != null)
            {
                ctx.VehicleType.Remove(existingtype);
                await ctx.SaveChangesAsync();
            }
            
        }

        public async Task<List<Pages.Vehicle.VehicleType>> GetAll()
        {
            var types = await ctx.VehicleType.ToListAsync();
            return types;
        }

        public  async Task<Pages.Vehicle.VehicleType> GetById(int id)
        {
            var brandFromDb = await ctx.VehicleType.FindAsync(id);
            // var brandFromDbFirst = ctx.Brand.FirstOrDefault(u => u.BrandId == id);
            //var brandFromDbSingle = ctx.Brand.SingleOrDefault(u => u.BrandId == id);

            return brandFromDb;
        }

        public async Task Update(Pages.Vehicle.VehicleType vehicle)
        {
            var existingType = await GetById(vehicle.VehicleId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(vehicle);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
