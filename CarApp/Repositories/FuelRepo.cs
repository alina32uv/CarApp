using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Pages.Fuels;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class FuelRepo : IFuel
    {
        private readonly CarAppContext ctx;

        public FuelRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string name)
        {
            await ctx.Fuel.AddAsync(new Fuel { Name = name });

            await ctx.SaveChangesAsync();
        }

        public async Task Delete(Fuel fuel)
        {
            var existingFuel = await GetById(fuel.FuelId);
            if (existingFuel != null)
            {
                ctx.Fuel.Remove(existingFuel);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Fuel>> GetAll()
        {
            var types = await ctx.Fuel.ToListAsync();
            return types;
        }

        public async Task<Fuel> GetById(int id)
        {
            var typeFromDb = await ctx.Fuel.FindAsync(id);

            return typeFromDb;
        }

        public async Task Update(Fuel fuel)
        {
            var existingType = await GetById(fuel.FuelId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(fuel);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
