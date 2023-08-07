using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Fuels.Commands
{
    public record CreateFuelCommand(string Name) : IRequest<Fuel>;

    public class CreateFuelHandler : IRequestHandler<CreateFuelCommand, Fuel>
    {
        private readonly CarAppContext ctx;

        public CreateFuelHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Fuel> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
        {
            var newFuel = new Fuel { Name = request.Name };
            await ctx.Fuel.AddAsync(newFuel);
            await ctx.SaveChangesAsync();

            return newFuel;
        }
    }

}
