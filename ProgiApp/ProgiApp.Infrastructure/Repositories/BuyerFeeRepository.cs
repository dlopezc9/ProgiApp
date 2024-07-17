using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;

namespace ProgiApp.Infrastructure.Repositories;

public class BuyerFeeRepository : IBuyerFeeRepository
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public BuyerFeeRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = this.configuration["ConnectionStrings:VehicleDBString"];
    }

    public async Task<IEnumerable<BuyerFee>> GetAll()
    {
        IEnumerable<BuyerFee> result;

        using (var db = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM BuyerFee";
            result = db.Query<BuyerFee>(sql);
            await db.DisposeAsync();
        }

        return result; 
    }
}
