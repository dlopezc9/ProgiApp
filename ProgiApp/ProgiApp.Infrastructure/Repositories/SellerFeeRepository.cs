using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;

namespace ProgiApp.Infrastructure.Repositories;

public class SellerFeeRepository : ISellerFeeRepository
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public SellerFeeRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = this.configuration["ConnectionStrings:VehicleDBString"];
    }

    public async Task<IEnumerable<SellerFee>> GetAll()
    {
        IEnumerable<SellerFee> result;

        using (var db = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM SellerFee";
            result = db.Query<SellerFee>(sql);
            await db.DisposeAsync();
        }

        return result; 
    }
}
