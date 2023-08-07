using CarApp.Data;
using CarApp.Entities;
using MediatR;

namespace CarApp.Pages.Fuels.Query
{
    public record GetByIdFuelQuery(int FuelId) : IRequest<Fuel>;
    public class GetFuelByIdHandler : IRequestHandler<GetByIdFuelQuery, Fuel>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetFuelByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Fuel> Handle(GetByIdFuelQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetFuelQuery());
            var output = await ctx.Fuel.FindAsync(request.FuelId);

            return output;
        }
    }
}
