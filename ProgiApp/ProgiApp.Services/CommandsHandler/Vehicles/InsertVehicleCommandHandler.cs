using MediatR;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Commands.Vehicles;

namespace ProgiApp.Services.CommandsHandler.Vehicles;

public class InsertVehicleCommandHandler : IRequestHandler<InsertVehicleCommand>
{
    private readonly IVehicleRepository vehicleRepository;

    public InsertVehicleCommandHandler(IVehicleRepository vehicleRepository)
    {
        this.vehicleRepository = vehicleRepository;
    }

    public async Task Handle(InsertVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle()
        {
            Id = request.vehicle.Id,
            TypeId = request.vehicle.TypeId,
            VehiclePrice = request.vehicle.VehiclePrice
        };

        await vehicleRepository.Add(vehicle);
    }
}
