using MediatR;
using ProgiApp.Domain.Models;

namespace ProgiApp.Services.Querys.Vehicles;

public record GetAllVehiclesQuery() : IRequest<VehicleList>;

public record VehicleList(IEnumerable<Vehicle> Vehicles);