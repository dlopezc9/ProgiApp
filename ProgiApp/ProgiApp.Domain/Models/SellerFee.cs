namespace ProgiApp.Domain.Models;

public class SellerFee
{
    public int Id { get; set; }
    public int? TypeId { get; set; }
    public decimal? FeeValue { get; set; }

}
