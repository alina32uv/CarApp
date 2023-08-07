using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.RentInfo.Queries;
using MediatR;

namespace CarApp.Pages.RentInfo.Commands
{
    public record UpdateRentInfoCommand(int RentInfoId, int CarId, string UserId, int StatusId, DateTime DateBring, DateTime DateReturn) : IRequest<RentInfo>;

    public class UpdateRentInfoHandler : IRequestHandler<UpdateRentInfoCommand, RentInfo>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateRentInfoHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;

        }
        public async Task<RentInfo> Handle(UpdateRentInfoCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await mediator.Send(new GetRentInfoByIdQuery(request.RentInfoId));
            if (existingOrder == null)
            {
                throw new Exception($"Order {request.RentInfoId} does not exist.");


            }

            existingOrder.CarId = request.CarId;
            existingOrder.UserId = request.UserId;
            existingOrder.StatusId = request.StatusId;
            existingOrder.DateBring = request.DateBring;
            existingOrder.DateReturn = request.DateReturn;


            ctx.RentInfo.Update(existingOrder);
            await ctx.SaveChangesAsync();

            return existingOrder;
        }
    }
}
