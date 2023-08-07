using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Body.Commands
{
    public record CreateBodyCommands(string Name) : IRequest<CarBodyType>;
    public class CreateBodyHandler : IRequestHandler<CreateBodyCommands, CarBodyType>
    {
        private readonly CarAppContext ctx;

        public CreateBodyHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<CarBodyType> Handle(CreateBodyCommands request, CancellationToken cancellationToken)
        {
            var newBody = new CarBodyType { Name = request.Name };
            await ctx.CarBodyType.AddAsync(newBody);
            await ctx.SaveChangesAsync();

            return newBody;
        }
    }
}
