using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.RentInfo.Queries
{
    public record GetRentInfoQyery() : IRequest<List<RentInfo>>;
    public class GetRentInfoHandler : IRequestHandler<GetRentInfoQyery, List<RentInfo>>
    {
        private readonly CarAppContext ctx;
        public GetRentInfoHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<List<RentInfo>> Handle(GetRentInfoQyery request, CancellationToken cancellationToken)
        {
            // return await ctx.RentInfo.ToListAsync();
            return await ctx.RentInfo
                     .Include(c => c.Car)
                     .Include(s => s.Status)
                     .ToListAsync();
        }
    }
}
