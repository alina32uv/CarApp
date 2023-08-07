using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Body.Query
{ public record GetBodyQuery() : IRequest<List<CarBodyType>>;

    public class GetBodyHandler:IRequestHandler<GetBodyQuery, List<CarBodyType>>
    {
        private readonly CarAppContext ctx;
        public GetBodyHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<CarBodyType>> Handle(GetBodyQuery request, CancellationToken cancellationToken)
        {
            return await ctx.CarBodyType.ToListAsync();
        }
    }
}
