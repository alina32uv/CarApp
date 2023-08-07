using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Fuels.Query;
using MediatR;

namespace CarApp.Pages.Fuels.Commands
{
    public record DeleteFuelCommand(int FuelId, string Name) : IRequest<Fuel>;
    public class DeleteFuelHandler : IRequestHandler<DeleteFuelCommand, Fuel>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteFuelHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Fuel> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            var existingFuel = await mediator.Send(new GetByIdFuelQuery(request.FuelId));
            if (existingFuel != null)
            {
                ctx.Fuel.Remove(existingFuel);
                await ctx.SaveChangesAsync();
            }
            return existingFuel;
        }
    }
}
