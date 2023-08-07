using CarApp.Data;
using CarApp.Pages.Brands.Queries;
using MediatR;

namespace CarApp.Pages.Brands.Queries
{
    public record GetBrandByIdQuery(int BrandId) : IRequest<Brand>;


    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, Brand>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetBrandByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }


        public async Task<Brand> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetBrandListQuery());
            var output = await ctx.Brand.FindAsync(request.BrandId);

            return output;
        }
    }

}
