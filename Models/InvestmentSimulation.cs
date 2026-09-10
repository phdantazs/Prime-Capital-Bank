using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;

public class InvestmentSimulation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SimulationCode { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public InvestmentType InvestmentType { get; set; }
    public decimal InitialInvestment { get; set; }
    public decimal ContributionAmount { get; set; }
    public ContributionFrequency ContributionFrequency { get; set; }
    public int Years { get; set; }
    public decimal AnnualRate { get; set; }
    public decimal TotalContributed { get; set; }
    public decimal InterestEarned { get; set; }
    public decimal FinalBalance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}