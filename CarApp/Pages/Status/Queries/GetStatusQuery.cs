using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Status.Queries
{
    public record GetStatusQuery() : IRequest<List<Status>>;
    public class GetStatusHandler : IRequestHandler<GetStatusQuery, List<Status>>
    {
        private readonly CarAppContext ctx;
        public GetStatusHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<List<Status>> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            return await ctx.Status.ToListAsync();
        }
    }
}
