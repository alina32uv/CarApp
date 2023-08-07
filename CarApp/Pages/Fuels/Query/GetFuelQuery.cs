using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Fuels.Query
{
    public record GetFuelQuery() : IRequest<List<Fuel>>;

    public class GetFuelHandler : IRequestHandler<GetFuelQuery, List<Fuel>>
    {
        private readonly CarAppContext ctx;
        public GetFuelHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Fuel>> Handle(GetFuelQuery request, CancellationToken cancellationToken)
        {
            return await ctx.Fuel.ToListAsync();
        }
    }
}
