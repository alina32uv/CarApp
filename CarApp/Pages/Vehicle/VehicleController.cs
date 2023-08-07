using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Vehicle.Commands;
using CarApp.Pages.Vehicle.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Pages.Vehicle
{
    public class VehicleController : Controller
    {

        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicle vehicleType;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;

        public VehicleController(ILogger<VehicleController> logger, IVehicle vehicleType, IMediator mediator)
        {
            _logger = logger;
            this.vehicleType = vehicleType;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _mediator.Send(new GetVehicleQuery());
            return View("/Pages/Vehicle/Views/Index.cshtml",types);
        }

        //GET
        public IActionResult Create()
        {
            return View("/Pages/Vehicle/Views/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(VehicleType vehicle)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateVehicleCommand(vehicle.Name));

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
            return View(vehicle);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var vehicleFromDb = await _mediator.Send(new GetVehicleByIdQuery(id));

            if (vehicleFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Vehicle/Views/Update.cshtml", vehicleFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VehicleType vehicle)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateVehicleCommand(vehicle.VehicleId, vehicle.Name));

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
            return View(vehicle);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var vehicleFromDb = await _mediator.Send(new GetVehicleByIdQuery(id));

            if (vehicleFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Vehicle/Views/Delete.cshtml", vehicleFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(VehicleType vehicle)
        {
            await _mediator.Send(new DeleteVehicleCommand(vehicle.VehicleId, vehicle.Name));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
