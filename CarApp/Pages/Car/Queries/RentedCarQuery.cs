using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car.Queries
{
    public record RentedCarQuery(int carId, string userId, DateTime currentDate) : IRequest<bool>;
    public class RentedCarHandler : IRequestHandler<RentedCarQuery, bool>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public RentedCarHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<bool> Handle(RentedCarQuery request, CancellationToken cancellationToken)
        {
            var existingRents = await ctx.RentInfo
                .Where(r => r.CarId == request.carId && r.UserId == request.userId)
                .ToListAsync();

            foreach (var rent in existingRents)
            {
                if (rent.DateReturn >= request.currentDate)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
