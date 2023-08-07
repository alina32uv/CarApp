using CarApp.Data;
using CarApp.Pages.Fuels.Query;
using MediatR;

namespace CarApp.Pages.Insurances.Queries
{ public record GetInsuranceByIdQuery(int InsuranceId):IRequest<Insurance>;
    public class GetInsuranceByIdHandler : IRequestHandler<GetInsuranceByIdQuery, Insurance>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetInsuranceByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Insurance> Handle(GetInsuranceByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetInsuranceQuery());
            var output = await ctx.Insurance.FindAsync(request.InsuranceId);

            return output;
        }
    }
}
