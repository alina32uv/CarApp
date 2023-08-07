using CarApp.Data;
using CarApp.Interfaces;
using CarApp.Pages.Brands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarApp.Repositories
{
    public class BrandRepo : ICarBrand
    {
        private readonly CarAppContext ctx;

        public BrandRepo(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task AddNew(string name)
        { 
           await ctx.Brand.AddAsync(new Brand { Name = name });   
          //  await ctx.Brand.AddAsync(brand);
            await ctx.SaveChangesAsync();
        }

       

        public async Task<List<Brand>> GetAll()
        {
            var brands =  await ctx.Brand.ToListAsync();
            return brands;

        }

        public async Task<Brand> GetById(int id)
        {

           
           var  brandFromDb = await ctx.Brand.FindAsync(id);
            // var brandFromDbFirst = ctx.Brand.FirstOrDefault(u => u.BrandId == id);
            //var brandFromDbSingle = ctx.Brand.SingleOrDefault(u => u.BrandId == id);

            return brandFromDb;
        }
        public async Task Delete(Brand brand)
        {
            var existingBrand = await GetById(brand.BrandId);
            if (existingBrand != null)
            {
                ctx.Brand.Remove(existingBrand);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task  Update(Brand brand)
        {

           /* var entityEntry = ctx.Brand.Update(brand);

            await ctx.SaveChangesAsync();*/
           var existingBrand = await GetById(brand.BrandId);
            if (existingBrand != null)
            {
                ctx.Entry(existingBrand).CurrentValues.SetValues(brand);
                ctx.Entry(existingBrand).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }

        }

        

       
    }
}
