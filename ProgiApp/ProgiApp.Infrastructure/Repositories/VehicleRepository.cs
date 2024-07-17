using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;

namespace ProgiApp.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public VehicleRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = this.configuration["ConnectionStrings:VehicleDBString"];
    }

    public async Task Add(Vehicle vehicle)
    {
        using (var db = new SqlConnection(connectionString))
        {
            var sql = "INSERT INTO dbo.Vehicle (typeId, vehiclePrice) VALUES(@type, @value)";
            var result =  db.Execute(sql, new { type = vehicle.TypeId, value = vehicle.VehiclePrice });
            await db.DisposeAsync();
        }
    }

    public async Task Delete(int id)
    {
        using (var db = new SqlConnection(connectionString))
        {
            var sql = "DELETE FROM dbo.Vehicle WHERE id = @id";
            var result = db.Execute(sql, new { id = id });
            await db.DisposeAsync();
        }
    }

    public async Task<IEnumerable<Vehicle>> GetAll()
    {
        IEnumerable<Vehicle> result;

        using (var db = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM Vehicle";
            result = db.Query<Vehicle>(sql);
            await db.DisposeAsync();
        }

        return result; 
    }
}
