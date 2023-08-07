using CarApp.Data;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Pages.Body;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class CarBodyTypeRepo : IBody
    {
        private readonly CarAppContext ctx;

        public CarBodyTypeRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string name)
        {
            await ctx.CarBodyType.AddAsync(new CarBodyType { Name = name });

            await ctx.SaveChangesAsync();
        }

        public async Task Delete(CarBodyType body)
        {
            var existingBody = await GetById(body.CarBodyTypeId);
            if (existingBody != null)
            {
                ctx.CarBodyType.Remove(existingBody);
                await ctx.SaveChangesAsync();
            }
        }

        public async  Task<List<CarBodyType>> GetAll()
        {
            var types = await ctx.CarBodyType.ToListAsync();
            return types;
        }

        public async  Task<CarBodyType> GetById(int id)
        {
            var typeFromDb = await ctx.CarBodyType.FindAsync(id);

            return typeFromDb;
        }

        public async Task Update(CarBodyType body)
        {
            var existingType = await GetById(body.CarBodyTypeId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(body);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
