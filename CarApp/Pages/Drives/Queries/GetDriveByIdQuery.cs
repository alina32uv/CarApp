using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Drives.Queries
{
    public record GetDriveByIdQuery(int DriveId) : IRequest<Drive>;
    public class GetDriveByIdHandler : IRequestHandler<GetDriveByIdQuery, Drive>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetDriveByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Drive> Handle(GetDriveByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetDriveQuery());
            var output = await ctx.Drive.FindAsync(request.DriveId);

            return output;
        }
    }
}
