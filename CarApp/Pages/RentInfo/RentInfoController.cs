using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.RentInfo.Commands;
using CarApp.Pages.RentInfo.Queries;
using CarApp.Pages.Status.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarApp.Pages.RentInfo
{
    public class RentInfoController : Controller
    {

        private readonly ILogger<RentInfoController> _logger;
        //private readonly IRentInfo orders;
        private readonly CarAppContext ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public RentInfoController(ILogger<RentInfoController> logger, IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            // this.orders = orders;
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //var types = await orders.GetAll();
            var types = await _mediator.Send(new GetRentInfoQyery());
            foreach (var rentInfo in types)
            {
                var user = await _userManager.FindByIdAsync(rentInfo.UserId);
                if (user != null)
                {
                    ViewBag.UserName = user.UserName;
                }
            }
            return View(types);
        }




        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var orderFromDb = await _mediator.Send(new GetRentInfoByIdQuery(id));

            if (orderFromDb == null)
            {
                return NotFound();
            }
            var distinctStatuses = await _mediator.Send(new GetStatusQuery());
            ViewBag.DistinctStatuses = distinctStatuses;
            return View(orderFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RentInfo order)
        {
            Console.WriteLine(order.StatusId);
            var user = order.UserId;
            ModelState.Remove("Car");
            ModelState.Remove("Status");
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateRentInfoCommand(order.RentInfoId, order.CarId, order.UserId, order.StatusId, order.DateBring, order.DateReturn));

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

            var distinctStatuses = await _mediator.Send(new GetStatusQuery());
            ViewBag.DistinctStatuses = distinctStatuses;
            return View(order);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var orderFromDb = await _mediator.Send(new GetRentInfoByIdQuery(id));

            if (orderFromDb == null)
            {
                return NotFound();
            }
            return View(orderFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(RentInfo order)
        {
            await _mediator.Send(new DeleteRentInfoCommand(order.RentInfoId, order.CarId, order.UserId, order.StatusId, order.DateBring, order.DateReturn));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
