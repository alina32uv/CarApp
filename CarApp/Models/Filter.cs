using CarApp.Entities;
using CarApp.Pages.Body;
using CarApp.Pages.Car;
using CarApp.Pages.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Models
{
    [Keyless]
    public class Filter
    {
        public List<Car> Cars{get; set;}
        public List<CarBodyType> CarBodyTypes { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }

    }
}
