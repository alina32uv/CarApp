using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Transmissions.Queries;
using MediatR;

namespace CarApp.Pages.Transmissions.Commands
{
    public record DeleteTansmissionCommand(int TransmissionId, string Type) : IRequest<Transmission>;
    public class DeleteTransmissionHandler : IRequestHandler<DeleteTansmissionCommand, Transmission>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteTransmissionHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Transmission> Handle(DeleteTansmissionCommand request, CancellationToken cancellationToken)
        {
            var existingType = await mediator.Send(new GetTransmissionByIdQuery(request.TransmissionId));
            if (existingType != null)
            {
                ctx.Transmission.Remove(existingType);
                await ctx.SaveChangesAsync();
            }
            return existingType;
        }
    }
}
