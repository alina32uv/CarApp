using CarApp.Data;
using CarApp.Migrations;
using CarApp.Pages.Car;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car.Queries
{
    public record FilterCarQuery(int vehicleId, int bodyId) : IRequest<List<Car>>;
    public class FilterCarHandler : IRequestHandler<FilterCarQuery, List<Car>>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public FilterCarHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<List<Car>> Handle(FilterCarQuery request, CancellationToken cancellationToken)
        {
            var filteredCars = await ctx.Car
               .Where(c => c.VehicleTypeId == request.vehicleId && c.CarBodyTypeId == request.bodyId)
               .Include(c => c.Fuel)
               .Include(c => c.Transmission)
               .Include(c => c.CarBodyType)
               .Include(c => c.VehicleType)
               .Include(c => c.Brand)
               .Include(c => c.Drive)
               .ToListAsync();

            return filteredCars;
        }
    }
}
