using ProgiApp.Domain.Models;
using Type = ProgiApp.Domain.Models.Type;

namespace ProgiApp.Domain.Interfaces;

public interface ITypeRepository
{
    Task<IEnumerable<Type>> GetAll();
}

