using Azure.Core;
using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Brands.Commands;
using CarApp.Pages.Brands.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CarApp.Pages.Brands
{
    public class RandController : Controller
    {

        private readonly ILogger<RandController> _logger;
        private readonly ICarBrand carBrand;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        private readonly string cacheKey = "brandCacheKey";


        public RandController(ILogger<RandController> logger, ICarBrand carBrand, IMediator mediator, IMemoryCache cache)
        {
            _logger = logger;
            this.carBrand = carBrand;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {

            var brands = new GetBrandListQuery();
            var result = await _mediator.Send(brands);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Brand> brand))
            {
                _logger.Log(LogLevel.Information, "Cars found in cache.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Cars not found in cache");
                brand = result;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, brand, cacheEntryOptions);

            }
            stopwatch.Stop();
            _logger.Log(LogLevel.Information, "Passed time " + stopwatch.ElapsedMilliseconds);


            return View("/Pages/Brands/Views/Index.cshtml", brand);
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
            return View("/Pages/Brands/Views/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateBrandCommand(brand.Name));
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(brand);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var brandFromDb = await _mediator.Send(new GetBrandByIdQuery(id));

            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Brands/Views/Update.cshtml", brandFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateBrandCommand(brand.BrandId, brand.Name));

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
            return View(brand);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var brandFromDb = await _mediator.Send(new GetBrandByIdQuery(id));

            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Brands/Views/Delete.cshtml", brandFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Brand brand)
        {

            await _mediator.Send(new DeleteBrandCommand(brand.BrandId, brand.Name));
            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
