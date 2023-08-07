using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Car;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Diagnostics;

namespace CarApp.Pages.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarView carItem;
        private readonly ICar carItem2;

        public HomeController(ILogger<HomeController> logger, ICarView carItem, ICar carItem2)
        {
            _logger = logger;
            this.carItem = carItem;
            this.carItem2 = carItem2;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult View1()
        {
            return View();
        }

        public async Task<IActionResult> ManageCars()
        {
            CarViewModel model = new CarViewModel();

            model.Fuels = await carItem.GetFuel();
            model.Drives = await carItem.GetDrive();
            model.carBodyTypes = await carItem.GetCarBody();
            model.vehicleTypes = await carItem.GetVehicleType();
            model.Brands = await carItem.GetBrand();
            model.Transmissions = await carItem.GetTransmission();


            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}