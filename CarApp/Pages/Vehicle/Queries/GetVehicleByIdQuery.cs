using CarApp.Data;
using MediatR;

namespace CarApp.Pages.Vehicle.Queries
{
    public record GetVehicleByIdQuery(int VehicleId) : IRequest<VehicleType>;
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, VehicleType>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetVehicleByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<VehicleType> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetVehicleQuery());
            var output = await ctx.VehicleType.FindAsync(request.VehicleId);

            return output;
        }
    }
}
