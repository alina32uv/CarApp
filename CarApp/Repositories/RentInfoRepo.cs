using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Models;
using CarApp.Pages.RentInfo;
using CarApp.Pages.Status;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarApp.Repositories
{
    public class RentInfoRepo : IRentInfo
    {
        private readonly CarAppContext ctx;

        public RentInfoRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task Delete(RentInfo rent)
        {
            var existingOrder = await GetById(rent.RentInfoId);

            if (existingOrder != null)
            {
                existingOrder.CarId = rent.CarId;
                existingOrder.UserId = rent.UserId;
                existingOrder.StatusId = rent.StatusId;
                existingOrder.DateBring = rent.DateBring;
                existingOrder.DateReturn = rent.DateReturn;

                //ctx.Entry(existingCar).State = EntityState.Modified;
                ctx.RentInfo.Remove(existingOrder);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<RentInfo>> GetAll()
        {
            var types = await ctx.RentInfo
                  .Include(c => c.Car)
                  .Include(s => s.Status)
                  .ToListAsync();
            return types;
        }

        public async Task<RentInfo> GetById(int id)
        {
            var orderFromDb = await ctx.RentInfo
          .Include(c => c.Car)
           .Include(c => c.Status)
          .FirstOrDefaultAsync(c => c.RentInfoId == id);

            if (orderFromDb != null)
            {
                return orderFromDb;
            }
            return null;
        }
        private async Task<RentInfo?> GetByIdTest(int id)
        {
            return await ctx.RentInfo.FindAsync(id);
        }
        public async Task<List<Status>> GetStatus()
        {
            return ctx.Status.ToList();
        }

        public async Task Update(RentInfo rent)
        {
            var existingOrder = await GetByIdTest(rent.RentInfoId);

            if (existingOrder != null)
            {
                existingOrder.CarId = rent.CarId;
                existingOrder.UserId = rent.UserId;
                existingOrder.StatusId = rent.StatusId;
                existingOrder.DateBring = rent.DateBring;
                existingOrder.DateReturn = rent.DateReturn;
          
                //ctx.Entry(existingCar).State = EntityState.Modified;
                ctx.RentInfo.Update(existingOrder);
                await ctx.SaveChangesAsync();
            }
           
                    Console.WriteLine(rent.StatusId);
            
        }



       


        /* public async Task UpdateModel(RentModel rent)
         {
             var existingOrder = await GetById(rent.CarId);

             if (existingOrder != null)
             {
                 existingOrder.RentInfoId = rent.RentInfoId;
                 existingOrder.CarId = rent.CarId;
                 existingOrder.UserId = rent.UserId;
                 existingOrder.StatusId = rent.StatusId;
                 existingOrder.DateBring = rent.DateBring;
                 existingOrder.DateReturn = rent.DateReturn;


                 ctx.Entry(existingOrder).State = EntityState.Modified;
                 await ctx.SaveChangesAsync();
             }
         }*/



    }
}
