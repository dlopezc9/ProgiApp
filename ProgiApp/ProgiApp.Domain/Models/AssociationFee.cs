namespace ProgiApp.Domain.Models;

public class AssociationFee
{
    public int Id { get; set; }
    public int? MinValue { get; set; }
    public int? MaxValue { get; set; }
    public int? FeeValue { get; set; }
}
