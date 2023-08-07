using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Drives.Commands
{
    public record CreateDriveCommand(string Name) : IRequest<Drive>;
    public class CreateDriveHandler : IRequestHandler<CreateDriveCommand, Drive>
    {
        private readonly CarAppContext ctx;

        public CreateDriveHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Drive> Handle(CreateDriveCommand request, CancellationToken cancellationToken)
        {
            var newDrive = new Drive { Name = request.Name };
            await ctx.Drive.AddAsync(newDrive);
            await ctx.SaveChangesAsync();

            return newDrive;
        }
    }
}
