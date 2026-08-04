using System.Dynamic;
using PrimeCapitalBank.Models.Enums;
namespace PrimeCapitalBank.Models;
public class Investment
{
    public InvestmentType Type { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal AnnualRate { get; set; }
    public DateTime InvestedAt { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}