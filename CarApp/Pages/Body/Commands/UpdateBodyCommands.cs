using CarApp.Pages.Fuels.Commands;
using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Fuels.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CarApp.Pages.Body.Query;

namespace CarApp.Pages.Body.Commands
{ public record UpdateBodyCommands(int CarBodyTypeId, string Name):IRequest<CarBodyType>;
    public class UpdateBodyHandler : IRequestHandler<UpdateBodyCommands, CarBodyType>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public UpdateBodyHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }

        public async Task<CarBodyType> Handle(UpdateBodyCommands request, CancellationToken cancellationToken)
        {
            var existingType = await mediator.Send(new GetBodyByIdQuery(request.CarBodyTypeId));
            if (existingType != null)
            {
                ctx.Entry(existingType).CurrentValues.SetValues(request);
                ctx.Entry(existingType).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingType;
        }
    }
}
