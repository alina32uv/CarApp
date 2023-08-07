using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Pages.Drives;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Repositories
{
    public class DriveRepo: IDrive
    {
        private readonly CarAppContext ctx;

        public DriveRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddNew(string name)
        {
            await ctx.Drive.AddAsync(new Drive { Name = name });
        
            await ctx.SaveChangesAsync();
        }

        public async Task Delete(Drive drive)
        {
            var existingDrive = await GetById(drive.DriveId);
            if (existingDrive != null)
            {
                ctx.Drive.Remove(existingDrive);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Drive>> GetAll()
        {
            var types = await ctx.Drive.ToListAsync();
            return types;
        }

        public async Task<Drive> GetById(int id)
        {
            var typeFromDb = await ctx.Drive.FindAsync(id);

            return typeFromDb;
        }

        public async Task Update(Drive drive)
        {
            var existingType = await GetById(drive.DriveId);
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(drive);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
