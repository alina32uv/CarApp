using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Transmissions.Commands;
using CarApp.Pages.Transmissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CarApp.Pages.Transmissions
{
    public class TransmissionController : Controller
    {

        private readonly ILogger<TransmissionController> _logger;
        private readonly ITransmission transmission;
        private readonly IMediator _mediator;

        private readonly IMemoryCache _cache;

        private readonly string cacheKey = "transmissionCacheKey";

        public TransmissionController(ILogger<TransmissionController> logger, ITransmission transmission, IMediator mediator, IMemoryCache cache)
        {
            _logger = logger;
            this.transmission = transmission;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _mediator.Send(new GetTransmissionQuery());


            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Transmission> transmission))
            {
                _logger.Log(LogLevel.Information, "Transmission types found in cache.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Transmission types not found in cache");
                transmission = types;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, transmission, cacheEntryOptions);

            }
            stopwatch.Stop();
            _logger.Log(LogLevel.Information, "Passed time " + stopwatch.ElapsedMilliseconds);

            return View("/Pages/Transmissions/Views/Index.cshtml", transmission);
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
            return View("/Pages/Transmissions/Views/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Transmission transmissiontype)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateTransmissionCommand(transmissiontype.Type));

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
            return View(transmissiontype);
        }

        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var typeFromDb = await _mediator.Send(new GetTransmissionByIdQuery(id));

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Transmissions/Views/Update.cshtml",typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Transmission transmissiontype)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateTransmissionCommand(transmissiontype.TransmissionId, transmissiontype.Type));

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
            return View(transmissiontype);
        }



        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var typeFromDb = await _mediator.Send(new GetTransmissionByIdQuery(id));

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Transmission/Views/Delete.cshtml",typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Transmission types)
        {
            await _mediator.Send(new DeleteTansmissionCommand(types.TransmissionId, types.Type));

            return RedirectToAction("Index");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
