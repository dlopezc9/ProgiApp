using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using Type = ProgiApp.Domain.Models.Type;

namespace ProgiApp.Infrastructure.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public TypeRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = this.configuration["ConnectionStrings:VehicleDBString"];
    }

    public async Task<IEnumerable<Type>> GetAll()
    {
        IEnumerable<Type> result;

        using (var db = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM Type";
            result = db.Query<Type>(sql);
            await db.DisposeAsync();
        }

        return result; 
    }
}
