using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Status.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Status.Commands
{
    public record UpdateStatusCommand(int StatusId, string Name) : IRequest<Status>;
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, Status>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateStatusHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Status> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var existingStatus = await mediator.Send(new GetStatusByIdQuery(request.StatusId));
            if (existingStatus != null)
            {
                ctx.Entry(existingStatus).CurrentValues.SetValues(request);
                ctx.Entry(existingStatus).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingStatus;
        }
    }
}
