using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Pages.Insurances;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class InsuranceRepo : IInsurance
    {
        private readonly CarAppContext ctx;

        public InsuranceRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string name)
        {
            await ctx.Insurance.AddAsync(new Insurance { Name = name });

            await ctx.SaveChangesAsync();
        }

        public async Task Delete(Insurance ins)
        {
            var existingType = await GetById(ins.InsuranceId);
            if (existingType != null)
            {
                ctx.Insurance.Remove(existingType);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Insurance>> GetAll()
        {
            var types = await ctx.Insurance.ToListAsync();
            return types;
        }

        public async Task<Insurance> GetById(int id)
        {
            var typeFromDb = await ctx.Insurance.FindAsync(id);

            return typeFromDb;
        }

        public async Task Update(Insurance ins)
        {
            var existingType = await GetById(ins.InsuranceId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(ins);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
