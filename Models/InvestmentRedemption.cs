namespace PrimeCapitalBank.Models;

public class InvestmentRedemption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid InvestmentId { get; set; }
    public Investment Investment { get; set; } = null!;
    public decimal GrossAmount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal ProfitAmount { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetAmount { get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}