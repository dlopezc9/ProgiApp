using MediatR;
using ProgiApp.Domain.Models;

namespace ProgiApp.Services.Commands.Vehicles;

public record DeleteVehicleCommand(int id) : IRequest;
