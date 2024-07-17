using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgiApp.Domain.Models;

public class Vehicle
{

    public int Id { get; set; }

    public int? TypeId { get; set; }

    public decimal? VehiclePrice { get; set; }

}
