using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;
public class Investment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string InvestmentCode { get; set; } = string.Empty;

    public Guid BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; } = null!;

    public InvestmentType Type { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public decimal AnnualRate { get; set; }
    public DateTime InvestedAt { get; set; } = DateTime.UtcNow;
    public InvestmentStatus Status { get; set; } = InvestmentStatus.Active;
    public DateTime? RedeemedAt { get; set; }
    
    public List<InvestmentRedemption> Redemptions { get; set; } = new();
}