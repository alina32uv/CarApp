using CarApp.Pages.Fuels.Commands;
using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Models;
using CarApp.Pages.Fuels.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace CarApp.Pages.Fuels
{
    public class FuelController : Controller
    {

        private readonly ILogger<FuelController> _logger;
        private readonly IFuel fuelType;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        private readonly string cacheKey = "fuelCacheKey";

        public FuelController(ILogger<FuelController> logger, IFuel fuelType, IMediator mediator, IMemoryCache cache)
        {
            _logger = logger;
            this.fuelType = fuelType;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {

            var types = await _mediator.Send(new GetFuelQuery());
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (_cache.TryGetValue(cacheKey, out IEnumerable<Fuel> fuel))
            {
                _logger.Log(LogLevel.Information, "Drive type found in cache.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Drive type not found in cache");
                fuel = types;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, fuel, cacheEntryOptions);

            }
            stopwatch.Stop();
            _logger.Log(LogLevel.Information, "Passed time " + stopwatch.ElapsedMilliseconds);

            return View(fuel);
        }
        public IActionResult ClearCache()
        {
            _cache.Remove(cacheKey);
            _logger.Log(LogLevel.Information, "Cleared cache");
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Create()
        {
            return View("/Pages/Fuels/Fuel/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateFuelCommand(fuel.Name));

                return RedirectToAction("Index");
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
            return View(fuel);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fuelFromDb = await _mediator.Send(new GetByIdFuelQuery(id));

            if (fuelFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Fuels/Fuel/Update.cshtml", fuelFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateFuelCommand(fuel.FuelId, fuel.Name));

                return RedirectToAction("Index");
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
            return View(fuel);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fuelFromDb = await _mediator.Send(new GetByIdFuelQuery(id));

            if (fuelFromDb == null)
            {
                return NotFound();
            }
            return View(fuelFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Fuel fuel)
        {


            await _mediator.Send(new DeleteFuelCommand(fuel.FuelId, fuel.Name));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
