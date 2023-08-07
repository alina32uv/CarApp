
using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Status.Commands;
using CarApp.Pages.Status.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Pages.Status
{
    public class StatusController : Controller
    {

        private readonly ILogger<StatusController> _logger;
        private readonly IStatus status;
        private readonly CarAppContext ctx;

        private readonly IMediator _mediator;

        public StatusController(ILogger<StatusController> logger, IStatus status, IMediator mediator)
        {
            _logger = logger;
            this.status = status;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _mediator.Send(new GetStatusQuery());
            return View("/Pages/Status/Views/Index.cshtml",types);
        }

        //GET
        public IActionResult Create()
        {
            return View("/Pages/Status/Views/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Status type)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateStatusCommand(type.Name));

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
            return View(type);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusFromDb = await _mediator.Send(new GetStatusByIdQuery(id));

            if (statusFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Status/Views/Update.cshtml",statusFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Status type)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateStatusCommand(type.StatusId, type.Name));

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
            return View(type);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusFromDb = await _mediator.Send(new GetStatusByIdQuery(id));

            if (statusFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Status/Views/Delete.cshtml",statusFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Status type)
        {
            await _mediator.Send(new DeleteStatusCommand(type.StatusId, type.Name));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
