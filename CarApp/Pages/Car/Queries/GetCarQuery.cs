using CarApp.Data;
using CarApp.Pages.Car;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car.Queries
{
    public record GetCarQuery() : IRequest<List<Car>>;
    public class GetCarHandler : IRequestHandler<GetCarQuery, List<Car>>
    {
        private readonly CarAppContext ctx;
        public GetCarHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<List<Car>> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var cars = await ctx.Car
       .Include(c => c.Fuel)
       .Include(c => c.Drive)
       .Include(c => c.CarBodyType)
       .Include(c => c.VehicleType)
       .Include(c => c.Brand)
       .Include(c => c.Transmission)
       .ToListAsync();
            return cars;
        }
    }
}
