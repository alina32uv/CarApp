using CarApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car.Queries
{
    public record IsCarAvailableQuery(int carId, DateTime dateBring, DateTime dateReturn) : IRequest<bool>;
    public class IsCarAvailableHandler : IRequestHandler<IsCarAvailableQuery, bool>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public IsCarAvailableHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<bool> Handle(IsCarAvailableQuery request, CancellationToken cancellationToken)
        {
            var existingRents = await ctx.RentInfo
                .Where(r => r.CarId == request.carId)
                .ToListAsync();

            foreach (var rent in existingRents)
            {
                if (!(rent.DateReturn <= request.dateBring || rent.DateBring >= request.dateReturn))
                {

                    return false;
                }
            }
            return true;
        }
    }
}
