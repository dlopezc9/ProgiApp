using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;

namespace ProgiApp.Infrastructure.Repositories;

public class AssociationFeeRepository : IAssociationFeeRepository
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public AssociationFeeRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = this.configuration["ConnectionStrings:VehicleDBString"];
    }

    public async Task<IEnumerable<AssociationFee>> GetAll()
    {
        IEnumerable<AssociationFee> result;

        using (var db = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM AssociationFee";
            result = db.Query<AssociationFee>(sql);
            await db.DisposeAsync();
        }

        return result; 
    }
}
