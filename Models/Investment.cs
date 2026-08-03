namespace PrimeCapitalBank.Models;
public class Investment
{
    public InvestmentType Type { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal AnnualRate { get; set; }
    public DateTime InvestedAt { get; set; }
}