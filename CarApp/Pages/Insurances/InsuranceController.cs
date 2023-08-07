using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Fuels.Commands;
using CarApp.Pages.Fuels;
using CarApp.Pages.Fuels.Query;
using CarApp.Pages.Insurances.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CarApp.Pages.Insurances.Commands;
using CarApp.Migrations;

namespace CarApp.Pages.Insurances
{
    public class InsuranceController : Controller
    {

        private readonly ILogger<InsuranceController> _logger;
        private readonly IInsurance insuranceType;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;

        public InsuranceController(ILogger<InsuranceController> logger, IInsurance insuranceType, IMediator mediator)
        {
            _logger = logger;
            this.insuranceType = insuranceType;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _mediator.Send(new GetInsuranceQuery());
            return View("/Pages/Insurances/Index.cshtml",types);
        }

        //GET
        public IActionResult Create()
        {
            return View("/Pages/Insurances/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateInsuranceCommand(insurance.Name));

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
            return View(insurance);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = await _mediator.Send(new GetInsuranceByIdQuery(id));

            if (insuranceFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Insurances/Update.cshtml",insuranceFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateInsuranceCommand(insurance.InsuranceId, insurance.Name));

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
            return View(insurance);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = await _mediator.Send(new GetInsuranceByIdQuery(id));

            if (insuranceFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Insurances/Delete.cshtml",insuranceFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Insurance insurance)
        {

            await _mediator.Send(new DeleteInsuranceCommand(insurance.InsuranceId, insurance.Name));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
