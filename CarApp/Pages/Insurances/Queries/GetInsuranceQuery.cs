using CarApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Insurances.Queries
{ 
    public record GetInsuranceQuery():IRequest<List<Insurance>>;
    public class GetInsuranceHandler : IRequestHandler<GetInsuranceQuery, List<Insurance>>
    {
        private readonly CarAppContext ctx;
        public GetInsuranceHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<List<Insurance>> Handle(GetInsuranceQuery request, CancellationToken cancellationToken)
        {
            return await ctx.Insurance.ToListAsync();
        }
    }
}
