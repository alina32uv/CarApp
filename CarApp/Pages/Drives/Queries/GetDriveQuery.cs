using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Drives.Queries
{
    public record GetDriveQuery() : IRequest<List<Drive>>;

    public class GetDriveHandler : IRequestHandler<GetDriveQuery, List<Drive>>
    {
        private readonly CarAppContext ctx;
        public GetDriveHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Drive>> Handle(GetDriveQuery request, CancellationToken cancellationToken)
        {
            return await ctx.Drive.ToListAsync();
        }
    }
}
