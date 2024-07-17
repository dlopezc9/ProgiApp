using MediatR;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Querys.Vehicles;

namespace ProgiApp.Services.QuerysHandler.Vehicles;

public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, VehicleList>
{
    private readonly IVehicleRepository _vehicleRepository;

    public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<VehicleList> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var result = await _vehicleRepository.GetAll();
        VehicleList vehicleList = new VehicleList(result);
        return vehicleList;
    }
}
