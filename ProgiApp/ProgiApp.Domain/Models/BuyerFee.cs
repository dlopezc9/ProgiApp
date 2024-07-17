namespace ProgiApp.Domain.Models;

public class BuyerFee
{ 
    public int Id { get; set; }

    public int TypeId { get; set; }

    public decimal? FeeValue { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

}
