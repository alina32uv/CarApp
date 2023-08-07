using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Transmissions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Transmissions.Commands
{
    public record UpdateTransmissionCommand(int TransmissionId, string Type) : IRequest<Transmission>;
    public class UpdateTransmissionHandler : IRequestHandler<UpdateTransmissionCommand, Transmission>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateTransmissionHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Transmission> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            var existingType = await mediator.Send(new GetTransmissionByIdQuery(request.TransmissionId));
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
