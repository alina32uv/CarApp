using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Drives.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Drives.Commands
{
    public record UpdateDiveCommand(int DriveId, string Name) : IRequest<Drive>;
    public class UpdateDriveHandler : IRequestHandler<UpdateDiveCommand, Drive>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateDriveHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Drive> Handle(UpdateDiveCommand request, CancellationToken cancellationToken)
        {
            var existingDrive = await mediator.Send(new GetDriveByIdQuery(request.DriveId));
            if (existingDrive != null)
            {
                ctx.Entry(existingDrive).CurrentValues.SetValues(request);
                ctx.Entry(existingDrive).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingDrive;
        }
    }
}
