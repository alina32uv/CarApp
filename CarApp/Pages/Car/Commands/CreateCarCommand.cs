using CarApp.Data;
using CarApp.Entities;
using CarApp.Pages.Car;
using MediatR;

namespace CarApp.Pages.Car.Commands
{
    public record CreateCarCommand(CarViewModel carViewModel) : IRequest<Unit>;

    public class CreateCarHandler : IRequestHandler<CreateCarCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public CreateCarHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Unit> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            /* ctx.Car.Add(request.Car);

             await ctx.SaveChangesAsync();*/
            var car = new Car
            {

                Name = request.carViewModel.Name,
                TransmissionId = request.carViewModel.TransmissionId,
                Seats = request.carViewModel.Seats,
                DailyPrice = request.carViewModel.DailyPrice,
                Year = request.carViewModel.Year,
                ModelName = request.carViewModel.ModelName,
                Image = request.carViewModel.Image,
                CarBodyTypeId = request.carViewModel.CarBodyTypeId,
                VehicleTypeId = request.carViewModel.VehicleTypeId,
                BrandId = (int)request.carViewModel.BrandId,
                FuelId = (int)request.carViewModel.FuelId,
                DriveId = (int)request.carViewModel.DriveId,

            };

            ctx.Car.Add(car);

            await ctx.SaveChangesAsync();

            return Unit.Value;
        }

    }
}
