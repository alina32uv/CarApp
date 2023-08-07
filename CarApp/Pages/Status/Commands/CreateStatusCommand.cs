using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Status.Commands
{
    public record CreateStatusCommand(string Name) : IRequest<Status>;
    public class CreateStatusHandler : IRequestHandler<CreateStatusCommand, Status>
    {
        private readonly CarAppContext ctx;

        public CreateStatusHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Status> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var newStatus = new Status { Name = request.Name };
            await ctx.Status.AddAsync(newStatus);
            await ctx.SaveChangesAsync();

            return newStatus;
        }
    }
}
