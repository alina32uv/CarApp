using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Transmissions.Commands
{
    public record CreateTransmissionCommand(string Type) : IRequest<Transmission>;
    public class CreateTransmissionHandler : IRequestHandler<CreateTransmissionCommand, Transmission>
    {
        private readonly CarAppContext ctx;

        public CreateTransmissionHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Transmission> Handle(CreateTransmissionCommand request, CancellationToken cancellationToken)
        {
            var newType = new Transmission { Type = request.Type };
            await ctx.Transmission.AddAsync(newType);
            await ctx.SaveChangesAsync();

            return newType;
        }
    }
}
