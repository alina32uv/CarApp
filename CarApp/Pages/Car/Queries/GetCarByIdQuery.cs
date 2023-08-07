using CarApp.Data;
using CarApp.Pages.Car;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car.Queries
{
    public record GetCarByIdQuery(int CarId) : IRequest<Car>;

    public class GetCarByIdHandler : IRequestHandler<GetCarByIdQuery, Car>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetCarByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var carFromDb = await ctx.Car
         .Include(c => c.Fuel)
         .Include(c => c.Transmission)
         .Include(c => c.CarBodyType)
         .Include(c => c.VehicleType)
         .Include(c => c.Brand)
         .Include(c => c.Drive)
         .FirstOrDefaultAsync(c => c.CarId == request.CarId);

            if (carFromDb != null)
            {
                return carFromDb;
            }
            return null;
        }
    }
}
