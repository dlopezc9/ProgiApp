using ProgiApp.Domain.Models;

namespace ProgiApp.Domain.Interfaces;

public interface ISellerFeeRepository
{
    Task<IEnumerable<SellerFee>> GetAll();
}

