using CarApp.Data;
using CarApp.Pages.Fuels.Query;
using CarApp.Pages.Insurances.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Insurances.Commands
{ public record UpdateInsuranceCommand(int InsuranceId, string Name):IRequest<Insurance>;
    public class UpdateInsuranceHandler : IRequestHandler<UpdateInsuranceCommand, Insurance>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateInsuranceHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Insurance> Handle(UpdateInsuranceCommand request, CancellationToken cancellationToken)
        {
            var existingInsurance = await mediator.Send(new GetInsuranceByIdQuery(request.InsuranceId));
            if (existingInsurance != null)
            {
                ctx.Entry(existingInsurance).CurrentValues.SetValues(request);
                ctx.Entry(existingInsurance).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingInsurance;
        }
    }
}
