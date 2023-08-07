using CarApp.Data;
using CarApp.Pages.Insurances;
using MediatR;

namespace CarApp.Pages.Insurances.Commands
{ public record CreateInsuranceCommand(string Name) : IRequest<Insurance>;
    public class CreateInsuranceHandler : IRequestHandler<CreateInsuranceCommand, Insurance>
    {
        private readonly CarAppContext ctx;

        public CreateInsuranceHandler(CarAppContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Insurance> Handle(CreateInsuranceCommand request, CancellationToken cancellationToken)
        {
            var newInsurance = new Insurance { Name = request.Name };
            await ctx.Insurance.AddAsync(newInsurance);
            await ctx.SaveChangesAsync();

            return newInsurance;
        }
    }
}
