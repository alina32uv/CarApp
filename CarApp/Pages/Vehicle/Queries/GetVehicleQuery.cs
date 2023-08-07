using CarApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Vehicle.Queries
{
    public record GetVehicleQuery() : IRequest<List<VehicleType>>;
    public class GetVehicleHandler : IRequestHandler<GetVehicleQuery, List<VehicleType>>
    {
        private readonly CarAppContext ctx;
        public GetVehicleHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<VehicleType>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            return await ctx.VehicleType.ToListAsync();
        }
    }
}
