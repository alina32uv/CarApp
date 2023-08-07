using CarApp.Data;
using CarApp.Pages.Fuels.Query;
using CarApp.Pages.Insurances.Queries;
using MediatR;

namespace CarApp.Pages.Insurances.Commands
{ public record DeleteInsuranceCommand(int InsuranceId, string Name):IRequest<Insurance>;
    public class DeleteInsuranceHandler : IRequestHandler<DeleteInsuranceCommand, Insurance>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteInsuranceHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Insurance> Handle(DeleteInsuranceCommand request, CancellationToken cancellationToken)
        {
            var existingInsurance = await mediator.Send(new GetInsuranceByIdQuery(request.InsuranceId));
            if (existingInsurance != null)
            {
                ctx.Insurance.Remove(existingInsurance);
                await ctx.SaveChangesAsync();
            }
            return existingInsurance;
        }
    }
}
