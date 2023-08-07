using CarApp.Data;
using CarApp.Entities;
using CarApp.Migrations;
using MediatR;

namespace CarApp.Pages.Body.Query
{ public record GetBodyByIdQuery(int CarBodyTypeId):IRequest<Entities.CarBodyType>;
    public class GetBodyByIdHandler : IRequestHandler<GetBodyByIdQuery, Entities.CarBodyType>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetBodyByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }

        public async Task<Entities.CarBodyType> Handle(GetBodyByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetBodyQuery());
            var output = await ctx.CarBodyType.FindAsync(request.CarBodyTypeId);

            return output;
        }
    }
}
