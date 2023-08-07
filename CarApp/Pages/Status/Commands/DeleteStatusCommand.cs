using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Status.Queries;
using MediatR;

namespace CarApp.Pages.Status.Commands
{
    public record DeleteStatusCommand(int StatusId, string Name) : IRequest<Status>;
    public class DeleteStatusHandler : IRequestHandler<DeleteStatusCommand, Status>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteStatusHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Status> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var existingStatus = await mediator.Send(new GetStatusByIdQuery(request.StatusId));
            if (existingStatus != null)
            {
                ctx.Status.Remove(existingStatus);
                await ctx.SaveChangesAsync();
            }
            return existingStatus;
        }
    }
}
