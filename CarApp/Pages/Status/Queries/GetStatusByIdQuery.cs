using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Status.Queries
{
    public record GetStatusByIdQuery(int StatusId) : IRequest<Status>;
    public class GetStatusByIdHandler : IRequestHandler<GetStatusByIdQuery, Status>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetStatusByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Status> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
        {

            var results = await _mediator.Send(new GetStatusQuery());
            var output = await ctx.Status.FindAsync(request.StatusId);

            return output;
        }
    }
}
