using CarApp.Data;
using CarApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Brands.Queries
{
    public record GetBrandListQuery() : IRequest<List<Brand>>;

    public class GetBrandListHandler : IRequestHandler<GetBrandListQuery, List<Brand>>
    {
        private readonly CarAppContext ctx;
        public GetBrandListHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Brand>> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
        {
            return await ctx.Brand.ToListAsync();
        }
    }

}
