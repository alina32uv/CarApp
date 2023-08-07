using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Fuels.Query;
using MediatR;

namespace CarApp.Pages.Transmissions.Queries
{
    public record GetTransmissionByIdQuery(int TransmissionId) : IRequest<Transmission>;
    public class GetTransmissionByIdHandler : IRequestHandler<GetTransmissionByIdQuery, Transmission>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetTransmissionByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Transmission> Handle(GetTransmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetTransmissionQuery());
            var output = await ctx.Transmission.FindAsync(request.TransmissionId);

            return output;
        }
    }
}
