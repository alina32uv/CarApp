using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Brands.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Brands.Commands
{
    public record DeleteBrandCommand(int BrandId, string Name) : IRequest<Brand>;

    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, Brand>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteBrandHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Brand> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await mediator.Send(new GetBrandByIdQuery(request.BrandId));
            if (existingBrand != null)
            {
                ctx.Brand.Remove(existingBrand);
                await ctx.SaveChangesAsync();
            }
            return existingBrand;
        }
    }



}