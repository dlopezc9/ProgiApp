using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;

namespace ProgiApp.Infrastructure.Repositories;

public class OtherFeeRepository : IOtherFeeRepository
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public OtherFeeRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = this.configuration["ConnectionStrings:VehicleDBString"];
    }

    public async Task<IEnumerable<OtherFee>> GetAll()
    {
        IEnumerable<OtherFee> result;

        using (var db = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM OtherFee";
            result = db.Query<OtherFee>(sql);
            await db.DisposeAsync();
        }

        return result; 
    }
}
