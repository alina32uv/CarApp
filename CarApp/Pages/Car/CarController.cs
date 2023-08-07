using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Body.Query;
using CarApp.Pages.Brands.Queries;
using CarApp.Pages.Car.Commands;
using CarApp.Pages.Car.Queries;
using CarApp.Pages.Drives.Queries;
using CarApp.Pages.Fuels.Query;
using CarApp.Pages.RentInfo;
using CarApp.Pages.Transmissions.Queries;
using CarApp.Pages.Vehicle.Queries;
using CarApp.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QRCoder;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;


namespace CarApp.Pages.Car
{
    public class CarController : Controller
    {

        private readonly ILogger<CarController> _logger;
        private readonly ICarView carItem;
        private readonly ICar carItem2;
        private readonly CarAppContext ctx;
        private IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        private readonly string cacheKey = "carCacheKey";


        public CarController(ILogger<CarController> logger, ICarView carItem, IWebHostEnvironment webHostEnvironment, ICar carItem2,
            UserManager<IdentityUser> userManager, IMediator mediator, IMemoryCache cache)
        {
            _logger = logger;
            this.carItem = carItem;
            this.webHostEnvironment = webHostEnvironment;
            this.carItem2 = carItem2;
            _userManager = userManager;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IActionResult> Index(string sortBy, string carBodyType)
        {
            var items = await _mediator.Send(new GetCarQuery());

            if (!string.IsNullOrEmpty(carBodyType))
            {
                items = items.Where(c => c.CarBodyType != null && c.CarBodyType.Name == carBodyType).ToList();
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                items = await _mediator.Send(new GetCarSortedQuery(sortBy));
            }

           


            return View(items);

        }


        public IActionResult ClearCache()
        {
            _cache.Remove(cacheKey);
            _logger.Log(LogLevel.Information, "Cleared cache");
            return RedirectToAction("IndexToUpdate");
        }


        public async Task<IActionResult> Sort(string sortBy)
        {
            var sortedCars = await _mediator.Send(new GetCarSortedQuery(sortBy));

            return View( sortedCars);
        }


        public async Task<IActionResult> Sort2(string sortBy)
        {
            var sortedCars = await _mediator.Send(new GetCarSortedQuery(sortBy));

            return View( sortedCars);
        }


        [HttpGet]
        public async Task<IActionResult> RentCar(int carId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }
            var car = await _mediator.Send(new GetCarByIdQuery(carId));
            if (car == null)
            {
                return NotFound();
            }


            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("LoginPage");
            }

            var userId = user.Id;

            var model = new RentModel
            {
                CarId = car.CarId,
                Car = car,
                DateBring = DateTime.Now.AddMinutes(1),
                DateReturn = DateTime.Now.AddDays(1),
                UserId = userId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RentCar(Pages.RentInfo.RentInfo model)
        {
            ModelState.Remove("Car");

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }
            }
            string userId = model.UserId;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {

                return RedirectToAction("Login", "Account");
            }


            model.UserId = user.Id;
            model.StatusId = 7;
            bool ver = false;
            if (model.DateBring < DateTime.Now)
            {
                return RedirectToAction("Result", ver);
            }


            var isCarAvailable = await _mediator.Send(new IsCarAvailableQuery(model.CarId, model.DateBring, model.DateReturn));
            if (!isCarAvailable)
            {
                return RedirectToAction("Result", new { error = "Car not available for the selected dates" });
            }

            var isCarAlreadyRented = await _mediator.Send(new RentedCarQuery(model.CarId, user.Id, DateTime.Now));
            if (isCarAlreadyRented)
            {
                return RedirectToAction("Result", new { error = "you cannot rent this car until the current rental period expires." });
            }
            await _mediator.Send(new RentCarCommand(model));
            // await carItem2.RentCar(model);

            return RedirectToAction("Result", model);
        }


        public async Task<IActionResult> Result(string error, RentModel model)
        {
            var car = await _mediator.Send(new GetCarByIdQuery(model.CarId));
            TimeSpan difference = model.DateReturn - model.DateBring;

            // int numberOfDays = difference.Days;
            double cost;
            if (difference.TotalHours < 24)
            {
                cost = (double)car.DailyPrice / 24 * difference.TotalHours;
            }
            else
            {

                cost = (double)car.DailyPrice * Math.Ceiling(difference.TotalDays);
            }



            model.StatusId = 1;
            if (car == null)
            {
                return NotFound();
            }

            ViewBag.ErrorMessage = error;

            if (string.IsNullOrEmpty(error))
            {
                var qrCodeText = "Car: " + car.Name + ";\n " +
                    "Year: " + car.Year + ";\n Vehicle type: " + car.VehicleType.Name +
                    ";\n Trasnmission type: " + car.Transmission.Type + ";\n" +
                    "Fuel type: " + car.Fuel.Name + ";\n Date-time PICK-UP car: " + model.DateBring
                    + ";\n Date-time RETURN car: " + model.DateReturn + ";\n Price/Day: " +
                    car.DailyPrice + ";\n Total amount: " + cost + "$.";
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new PngByteQRCode(qrCodeData);

                var qrCodeBytes = qrCode.GetGraphic(20);
                ViewBag.QRCodeImageBytes = qrCodeBytes;
                return File(qrCodeBytes, "image/png");
            }

            return View();

        }



        public async Task<IActionResult> IndexToUpdate(string sortBy, string carBodyType)
        {
            var items = await _mediator.Send(new GetCarQuery());
            if (!string.IsNullOrEmpty(carBodyType))
            {
                items = items.Where(c => c.CarBodyType != null && c.CarBodyType.Name == carBodyType).ToList();
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                items = await _mediator.Send(new GetCarSortedQuery(sortBy));
            }


            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Car> cars))
            {
                _logger.Log(LogLevel.Information, "Cars found in cache.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Cars not found in cache");
                cars = items;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, cars, cacheEntryOptions);

            }
            stopwatch.Stop();
            _logger.Log(LogLevel.Information, "Passed time " + stopwatch.ElapsedMilliseconds);



            return View(cars);
        }


        public async Task<IActionResult> FilterCars2(IEnumerable<int> vehicleTypes, IEnumerable<int> carBodyTypes,
            IEnumerable<int> Brands, IEnumerable<int> fuelTypes, IEnumerable<int> driveTypes,
            IEnumerable<int> transmissionTypes)
        {
            var filteredCars = await _mediator.Send(new FilterCar2Query(vehicleTypes, carBodyTypes, Brands, fuelTypes, driveTypes, transmissionTypes));
            return View("Index", filteredCars);
        }




        public async Task<IActionResult> FilterCars3(IEnumerable<int> vehicleTypes, IEnumerable<int> carBodyTypes,
           IEnumerable<int> Brands, IEnumerable<int> fuelTypes, IEnumerable<int> driveTypes,
           IEnumerable<int> transmissionTypes)
        {
            var filteredCars = await _mediator.Send(new FilterCar2Query(vehicleTypes, carBodyTypes, Brands, fuelTypes, driveTypes, transmissionTypes));
            return View("IndexToUpdate", filteredCars);
        }


        public async Task<IActionResult> FilterCars(int vehicleId, int bodyId)
        {
            if (vehicleId != null && bodyId != 0)
            {
                var filteredCars = await _mediator.Send(new FilterCarQuery(vehicleId, bodyId));


                return View("IndexToUpdate", filteredCars);
            }
            return View("IndexToUpdate");

        }

        //GET
        public async Task<IActionResult> Create()
        {
            CarViewModel model = new CarViewModel();

            model.Fuels = await _mediator.Send(new GetFuelQuery());
            model.Drives = await _mediator.Send(new GetDriveQuery());
            model.carBodyTypes = await _mediator.Send(new GetBodyQuery());
            model.vehicleTypes = await _mediator.Send(new GetVehicleQuery());
            model.Brands = await _mediator.Send(new GetBrandListQuery());
            model.Transmissions = await _mediator.Send(new GetTransmissionQuery());


            return View(model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CarViewModel carViewModel, IFormFile image)
        {
            //ModelState.Remove(nameof(carViewModel.Brands));
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "img", imageName);
                    carViewModel.Image = imageName;

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }


                }

                // await carItem.AddNew(carViewModel); 
                await _mediator.Send(new CreateCarCommand(carViewModel));

                return RedirectToAction("IndexToUpdate");
            }
            else
            {

            }

            return View(carViewModel);
        }



        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Preluare model pentru afișare
            var model = new CarViewModel();

            model.Fuels = await _mediator.Send(new GetFuelQuery());
            model.Drives = await _mediator.Send(new GetDriveQuery());
            model.carBodyTypes = await _mediator.Send(new GetBodyQuery());
            model.vehicleTypes = await _mediator.Send(new GetVehicleQuery());
            model.Brands = await _mediator.Send(new GetBrandListQuery());
            model.Transmissions = await _mediator.Send(new GetTransmissionQuery());

            // Preluare obiectul CarViewModel pentru actualizare
            var carFromDb = await _mediator.Send(new GetCarByIdQuery(id));

            if (carFromDb == null)
            {
                return NotFound();
            }

            // Setarea valorilor existente în modelul de vizualizare
            model.CarId = carFromDb.CarId;
            model.Name = carFromDb.Name;
            model.TransmissionId = carFromDb.TransmissionId;
            model.Seats = carFromDb.Seats;
            model.DailyPrice = carFromDb.DailyPrice;
            model.Year = carFromDb.Year;
            model.ModelName = carFromDb.ModelName;
            model.Image = carFromDb.Image;
            model.CarBodyTypeId = carFromDb.CarBodyTypeId;
            model.VehicleTypeId = (int)carFromDb.VehicleTypeId;
            model.BrandId = carFromDb.BrandId;
            model.FuelId = carFromDb.FuelId;
            model.DriveId = carFromDb.DriveId;

            return View(model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CarViewModel carViewModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "img", imageName);
                    carViewModel.Image = imageName;

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }


                }

                await _mediator.Send(new UpdateCarCommand(carViewModel));

                return RedirectToAction("IndexToUpdate");
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        // Afișează mesajele de eroare pentru depanare
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(carViewModel);
        }



        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Preluare model pentru afișare
            var model = new CarViewModel();

            model.Fuels = await _mediator.Send(new GetFuelQuery());
            model.Drives = await _mediator.Send(new GetDriveQuery());
            model.carBodyTypes = await _mediator.Send(new GetBodyQuery());
            model.vehicleTypes = await _mediator.Send(new GetVehicleQuery());
            model.Brands = await _mediator.Send(new GetBrandListQuery());
            model.Transmissions = await _mediator.Send(new GetTransmissionQuery());

            // Preluare obiectul CarViewModel pentru stergere
            var carFromDb = await _mediator.Send(new GetCarByIdQuery(id));

            if (carFromDb == null)
            {
                return NotFound();
            }

            // Setarea valorilor existente în modelul de vizualizare
            model.CarId = carFromDb.CarId;
            model.Name = carFromDb.Name;
            model.TransmissionId = carFromDb.TransmissionId;
            model.Seats = carFromDb.Seats;
            model.DailyPrice = carFromDb.DailyPrice;
            model.Year = carFromDb.Year;
            model.ModelName = carFromDb.ModelName;
            model.Image = carFromDb.Image;
            model.CarBodyTypeId = carFromDb.CarBodyTypeId;
            model.VehicleTypeId = (int)carFromDb.VehicleTypeId;
            model.BrandId = carFromDb.BrandId;
            model.FuelId = carFromDb.FuelId;
            model.DriveId = carFromDb.DriveId;


            return View(model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(carViewModel.Image))
                {
                    // Obține calea către imaginea existentă
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "img", carViewModel.Image);

                    // Șterge fișierul imagine
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                await _mediator.Send(new DeleteCarCommand(carViewModel));

                return RedirectToAction("IndexToUpdate");
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        // Afișează mesajele de eroare pentru depanare
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(carViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
