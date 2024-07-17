using MediatR;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Commands.Vehicles;

namespace ProgiApp.Services.CommandsHandler.Vehicles;

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
{
    private readonly IVehicleRepository _vehicleRepository;

    public DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        await _vehicleRepository.Delete(request.id);
    }
}