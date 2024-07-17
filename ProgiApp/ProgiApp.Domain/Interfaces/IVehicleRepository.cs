using ProgiApp.Domain.Models;

namespace ProgiApp.Domain.Interfaces;

public interface IVehicleRepository
{
    Task Add(Vehicle vehicle);
    Task<IEnumerable<Vehicle>> GetAll();

    Task Delete(int id);
}
