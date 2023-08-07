using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Body.Query;
using MediatR;

namespace CarApp.Pages.Body.Commands
{
    public record DeleteBodyCommands(int CarBodyTypeId, string Name) : IRequest<CarBodyType>;
    public class DeleteBodyHandler : IRequestHandler<DeleteBodyCommands, CarBodyType>
    {
        private readonly CarAppContext ctx;
        private readonly IMediator mediator;

        public DeleteBodyHandler(CarAppContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<CarBodyType> Handle(DeleteBodyCommands request, CancellationToken cancellationToken)
        {
            var existingType = await mediator.Send(new GetBodyByIdQuery(request.CarBodyTypeId));
            if (existingType != null)
            {
                ctx.CarBodyType.Remove(existingType);
                await ctx.SaveChangesAsync();
            }
            return existingType;
        }
    }
}
