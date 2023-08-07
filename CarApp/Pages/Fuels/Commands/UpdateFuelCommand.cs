using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Fuels.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Fuels.Commands
{
    public record UpdateFuelCommand(int FuelId, string Name) : IRequest<Fuel>;
    public class UpdateFuelHandler : IRequestHandler<UpdateFuelCommand, Fuel>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateFuelHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Fuel> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            var existingFuel = await mediator.Send(new GetByIdFuelQuery(request.FuelId));
            if (existingFuel != null)
            {
                ctx.Entry(existingFuel).CurrentValues.SetValues(request);
                ctx.Entry(existingFuel).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingFuel;
        }
    }

}
