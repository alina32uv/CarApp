using CarApp.Data;
using CarApp.Pages.Vehicle.Queries;
using MediatR;

namespace CarApp.Pages.Vehicle.Commands
{
    public record DeleteVehicleCommand(int VehicleId, string Name) : IRequest<VehicleType>;
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, VehicleType>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteVehicleHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<VehicleType> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var existingType = await mediator.Send(new GetVehicleByIdQuery(request.VehicleId));
            if (existingType != null)
            {
                ctx.VehicleType.Remove(existingType);
                await ctx.SaveChangesAsync();
            }
            return existingType;
        }
    }
}
