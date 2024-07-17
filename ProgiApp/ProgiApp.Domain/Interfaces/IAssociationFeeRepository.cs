using ProgiApp.Domain.Models;

namespace ProgiApp.Domain.Interfaces;

public interface IAssociationFeeRepository
{
    Task<IEnumerable<AssociationFee>> GetAll();
}
