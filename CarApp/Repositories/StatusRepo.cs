using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Pages.Status;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class StatusRepo: IStatus
    {
        private readonly CarAppContext ctx;

        public StatusRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string name)
        {
            await ctx.Status.AddAsync(new Status { Name = name });
            
            await ctx.SaveChangesAsync();
        }

        public async Task Delete(Status status)
        {
            var existingStatus = await GetById(status.StatusId);
            if (existingStatus != null)
            {
                ctx.Status.Remove(existingStatus);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Status>> GetAll()
        {
            var types = await ctx.Status.ToListAsync();
            return types;
        }

        public async Task<Status> GetById(int id)
        {
            var typeFromDb = await ctx.Status.FindAsync(id);

            return typeFromDb; 
        }

        public async Task Update(Status status)
        {
            var existingType = await GetById(status.StatusId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(status);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
