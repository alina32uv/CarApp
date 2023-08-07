using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Transmissions.Queries
{
    public record GetTransmissionQuery() : IRequest<List<Transmission>>;
    public class GetTransmissionHandler : IRequestHandler<GetTransmissionQuery, List<Transmission>>
    {
        private readonly CarAppContext ctx;
        public GetTransmissionHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<List<Transmission>> Handle(GetTransmissionQuery request, CancellationToken cancellationToken)
        {
            return await ctx.Transmission.ToListAsync();
        }
    }
}
