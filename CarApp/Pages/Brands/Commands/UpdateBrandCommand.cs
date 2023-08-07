using CarApp.Data;
using CarApp.Entities;
using CarApp.Migrations;
using CarApp.Pages.Brands.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Brands.Commands
{

    public record UpdateBrandCommand(int BrandId, string Name) : IRequest<Brand>;
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, Brand>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateBrandHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Brand> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await mediator.Send(new GetBrandByIdQuery(request.BrandId));
            if (existingBrand != null)
            {
                ctx.Entry(existingBrand).CurrentValues.SetValues(request);
                ctx.Entry(existingBrand).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingBrand;
        }
    }
}
