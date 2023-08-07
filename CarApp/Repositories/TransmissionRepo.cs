using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Pages.Transmissions;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class TransmissionRepo : ITransmission
    {
        private readonly CarAppContext ctx;

        public TransmissionRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string type)
        {
            await ctx.Transmission.AddAsync(new Transmission { Type = type });
            //  await ctx.Brand.AddAsync(brand);
            await ctx.SaveChangesAsync();
        }

        public async Task Delete(Transmission transmission)
        {
            var existingType = await GetById(transmission.TransmissionId);
            if (existingType != null)
            {
                ctx.Transmission.Remove(existingType);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Transmission>> GetAll()
        {

            var types = await ctx.Transmission.ToListAsync();
            return types;
        }

        public async Task<Transmission> GetById(int id)
        {
            var typeFromDb = await ctx.Transmission.FindAsync(id);
            // var brandFromDbFirst = ctx.Brand.FirstOrDefault(u => u.BrandId == id);
            //var brandFromDbSingle = ctx.Brand.SingleOrDefault(u => u.BrandId == id);

            return typeFromDb; throw new NotImplementedException();
        }

        public async Task Update(Transmission transmission)
        {
            var existingType = await GetById(transmission.TransmissionId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(transmission);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
