using CarApp.Data;
using MediatR;

namespace CarApp.Pages.Vehicle.Commands
{
    public record CreateVehicleCommand(string Name) : IRequest<VehicleType>;
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleCommand, VehicleType>
    {
        private readonly CarAppContext ctx;

        public CreateVehicleHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<VehicleType> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var newType = new VehicleType { Name = request.Name };
            await ctx.VehicleType.AddAsync(newType);
            await ctx.SaveChangesAsync();

            return newType;
        }
    }
}
