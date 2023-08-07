using CarApp.Data;
using CarApp.Migrations;
using CarApp.Models;
using CarApp.Pages.Car;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CarApp.Pages.Car.Queries
{
    public record FilterCar2Query(IEnumerable<int> vehicleTypes, IEnumerable<int> carBodyTypes,
            IEnumerable<int> Brands, IEnumerable<int> fuelTypes, IEnumerable<int> driveTypes,
            IEnumerable<int> transmissionTypes) : IRequest<List<Car>>;
    public class FilterCar2Handler : IRequestHandler<FilterCar2Query, List<Car>>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public FilterCar2Handler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<List<Car>> Handle(FilterCar2Query request, CancellationToken cancellationToken)
        {

            var filteredVehicleTypes = request.vehicleTypes.ToList();
            var filteredCarBodyTypes = request.carBodyTypes.ToList();
            var filteredBrands = request.Brands.ToList();
            var filteredFuelTypes = request.fuelTypes.ToList();
            var filteredDriveTypes = request.driveTypes.ToList();
            var filteredTransmissionTypes = request.transmissionTypes.ToList();

            if (!filteredVehicleTypes.Any() || !filteredCarBodyTypes.Any() || !filteredBrands.Any() ||
    !filteredFuelTypes.Any() || !filteredDriveTypes.Any() || !filteredTransmissionTypes.Any())
            {
                // Returnează toate mașinile sau un rezultat gol, în funcție de necesități
                return await ctx.Car.ToListAsync();
            }

            var filteredCars = await ctx.Car
                .Where(c => filteredVehicleTypes.Contains(c.VehicleTypeId)
                && filteredCarBodyTypes.Contains(c.CarBodyTypeId) && filteredBrands.Contains(c.BrandId)
                && filteredFuelTypes.Contains(c.FuelId) && filteredDriveTypes.Contains(c.DriveId)
                && filteredTransmissionTypes.Contains(c.TransmissionId)
                )
                .Include(c => c.Fuel)
                .Include(c => c.Transmission)
                .Include(c => c.CarBodyType)
                .Include(c => c.VehicleType)
                .Include(c => c.Brand)
                .Include(c => c.Drive)
                .ToListAsync();

            return filteredCars;
        }
    }
}
