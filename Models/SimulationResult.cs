using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;

public class SimulationResult
{
    public InvestmentType InvestmentType { get; set; }
    public decimal InitialInvestment { get; set; }
    public decimal ContributionAmount { get; set; }
    public ContributionFrequency ContributionFrequency { get; set; }
    public int Years { get; set; }
    public decimal AnnualRate { get; set; }
    public decimal TotalContributed { get; set; }
    public decimal InterestEarned { get; set; }
    public decimal FinalBalance { get; set; }
}