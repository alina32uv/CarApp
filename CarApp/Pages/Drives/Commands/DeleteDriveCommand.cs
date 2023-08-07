using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Drives.Queries;
using MediatR;

namespace CarApp.Pages.Drives.Commands
{
    public record DeleteDriveCommand(int DriveId, string Name) : IRequest<Drive>;
    public class DeleteDriveHandler : IRequestHandler<DeleteDriveCommand, Drive>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteDriveHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Drive> Handle(DeleteDriveCommand request, CancellationToken cancellationToken)
        {
            var existingDrive = await mediator.Send(new GetDriveByIdQuery(request.DriveId));
            if (existingDrive != null)
            {
                ctx.Drive.Remove(existingDrive);
                await ctx.SaveChangesAsync();
            }
            return existingDrive;
        }
    }
}
