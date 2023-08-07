using CarApp.Entities;
using CarApp.Pages.Body;
using CarApp.Pages.Brands;
using CarApp.Pages.Car;
using CarApp.Pages.Drives;
using CarApp.Pages.Fuels;
using CarApp.Pages.Insurances;
using CarApp.Pages.RentInfo;
using CarApp.Pages.Status;
using CarApp.Pages.Transmissions;
using CarApp.Pages.Vehicle;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace CarApp.Data
{
    public class CarAppContext: IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public CarAppContext(DbContextOptions<CarAppContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<CarViewModel> CarViewModel { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Transmission> Transmission { get; set; }
        public DbSet<CarBodyType> CarBodyType { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        //public DbSet<Filter> Filter { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<RentInfo> RentInfo { get; set; }
        public DbSet<RentModel> RentModel { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Drive> Drive { get; set; }
        public DbSet<Fuel> Fuel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
