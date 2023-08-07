using CarApp.Entities;
using CarApp.Pages.Body;
using CarApp.Pages.Brands;
using CarApp.Pages.Drives;
using CarApp.Pages.Fuels;
using CarApp.Pages.Transmissions;
using CarApp.Pages.Vehicle;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Pages.Car
{
    public class CarViewModel
    {
        [Key]
        public int CarId { get; set; }


        public string Name { get; set; }
        public int TransmissionId { get; set; }
        public int Seats { get; set; }
        public int? DriveId { get; set; }
        public decimal DailyPrice { get; set; }
        public string Year { get; set; }
        public string ModelName { get; set; }
        public string? Image { get; set; }
        public int CarBodyTypeId { get; set; }
        public int VehicleTypeId { get; set; }
        public int? BrandId { get; set; }
        public int? FuelId { get; set; }
        public bool? isBodyTypeSelected { get; set; }
        // public bool? isVehicleTypeSelected { get; set; }

        public IEnumerable<Fuel>? Fuels { get; set; }
        public IEnumerable<Transmission>? Transmissions { get; set; }
        public IEnumerable<CarBodyType>? carBodyTypes { get; set; }
        public IEnumerable<VehicleType>? vehicleTypes { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public IEnumerable<Drive>? Drives { get; set; }



    }
}
