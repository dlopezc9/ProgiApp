using ProgiApp.Domain.Models;
using Type = ProgiApp.Domain.Models.Type;

namespace ProgiApp.Domain.Interfaces;

public interface IBuyerFeeRepository
{
    Task<IEnumerable<BuyerFee>> GetAll();
}
