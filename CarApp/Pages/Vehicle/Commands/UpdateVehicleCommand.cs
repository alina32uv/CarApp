using CarApp.Data;
using CarApp.Pages.Vehicle.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Vehicle.Commands
{
    public record UpdateVehicleCommand(int VehicleId, string Name) : IRequest<VehicleType>;

    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, VehicleType>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateVehicleHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<VehicleType> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var existingType = await mediator.Send(new GetVehicleByIdQuery(request.VehicleId));
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(request);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingType;
        }
    }
}
