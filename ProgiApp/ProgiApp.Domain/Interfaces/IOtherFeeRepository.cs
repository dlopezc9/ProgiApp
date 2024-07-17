using ProgiApp.Domain.Models;

namespace ProgiApp.Domain.Interfaces;

public interface IOtherFeeRepository
{
    Task<IEnumerable<OtherFee>> GetAll();
}

