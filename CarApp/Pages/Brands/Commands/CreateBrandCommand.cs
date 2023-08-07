using CarApp.Data;
using CarApp.Entities;
using MediatR;
using System.Reflection.Metadata;

namespace CarApp.Pages.Brands.Commands
{

    public record CreateBrandCommand(string Name) : IRequest<Brand>;
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Brand>
    {
        private readonly CarAppContext ctx;

        public CreateBrandHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var newBrand = new Brand { Name = request.Name };
            await ctx.Brand.AddAsync(newBrand);
            await ctx.SaveChangesAsync();

            return newBrand;
        }
    }


}
