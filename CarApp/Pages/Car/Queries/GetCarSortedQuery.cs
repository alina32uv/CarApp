using CarApp.Data;
using CarApp.Pages.Car;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CarApp.Pages.Car.Queries
{
    public record GetCarSortedQuery(string sortBy) : IRequest<List<Car>>;
    public class GetCarSortedHandler : IRequestHandler<GetCarSortedQuery, List<Car>>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetCarSortedHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<List<Car>> Handle(GetCarSortedQuery request, CancellationToken cancellationToken)
        {
            var sortedCars = ctx.Car.AsQueryable();
            switch (request.sortBy)
            {
                case "Price1":
                    sortedCars = sortedCars.OrderBy(c => c.DailyPrice);
                    break;
                case "Price2":
                    sortedCars = sortedCars.OrderByDescending(c => c.DailyPrice);
                    break;
                case "Year2":
                    sortedCars = sortedCars.OrderBy(c => c.Year);
                    break;
                case "Year1":
                    sortedCars = sortedCars.OrderByDescending(c => c.Year);
                    break;
                default:
                    sortedCars = sortedCars.OrderBy(c => c.Name);
                    break;
            }

            return await sortedCars.ToListAsync();
        }
    }
}
