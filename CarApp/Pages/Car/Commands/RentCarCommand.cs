using CarApp.Data;
using CarApp.Entities;
using CarApp.Models;
using MediatR;

namespace CarApp.Pages.Car.Commands
{
    public record RentCarCommand(Pages.RentInfo.RentInfo rent) : IRequest<Unit>;
    public class RentCarHandler : IRequestHandler<RentCarCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public RentCarHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Unit> Handle(RentCarCommand request, CancellationToken cancellationToken)
        {
            var rentInfo = new Pages.RentInfo.RentInfo
            {
                CarId = request.rent.CarId,
                DateBring = request.rent.DateBring,
                DateReturn = request.rent.DateReturn,
                UserId = request.rent.UserId,
                StatusId = request.rent.StatusId
            };

            ctx.RentInfo.Add(rentInfo);
            await ctx.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
