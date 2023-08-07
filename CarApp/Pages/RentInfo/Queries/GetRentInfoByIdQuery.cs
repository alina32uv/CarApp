using CarApp.Data;
using CarApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.RentInfo.Queries
{
    public record GetRentInfoByIdQuery(int RentInfoId) : IRequest<RentInfo>;
    public class GetRentInfoByIdHandler : IRequestHandler<GetRentInfoByIdQuery, RentInfo>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public GetRentInfoByIdHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<RentInfo> Handle(GetRentInfoByIdQuery request, CancellationToken cancellationToken)
        {
            /*var results = await _mediator.Send(new GetRentInfoQyery());
            var output = await ctx.RentInfo.FindAsync(request.RentInfoId);

            return output;*/
            var orderFromDb = await ctx.RentInfo
         .Include(c => c.Car)
          .Include(c => c.Status)
         .FirstOrDefaultAsync(c => c.RentInfoId == request.RentInfoId);

            if (orderFromDb != null)
            {
                return orderFromDb;
            }
            return null;
        }
    }
}
