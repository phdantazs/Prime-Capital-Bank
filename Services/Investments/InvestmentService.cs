using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;
namespace PrimeCapitalBank.Services.Investments;

public class InvestmentService
{
    public decimal GetAnnualRate(InvestmentType investmentType)
    {
        return investmentType switch
        {
            InvestmentType.TreasurySelic => 0.1120m,
            InvestmentType.CDB => 0.1240m,
            InvestmentType.LCI => 0.1080m,
            InvestmentType.LCA => 0.1090m,
            InvestmentType.FixedIncomeFund => 0.1020m,

            _ => throw new ArgumentOutOfRangeException(nameof(investmentType), "Invalid investment type.")
        };
    }
    public void Invest(BankAccount account)
    {
        Console.Clear();

        Console.WriteLine("========== INVESTMENTS ==========\n");
        Console.WriteLine("Choose an investment:\n");
        
        Console.WriteLine("1 - Treasury Selic");
        Console.WriteLine("2 - CDB");
        Console.WriteLine("3 - LCI");
        Console.WriteLine("4 - LCA");
        Console.WriteLine("5 - Fixed Income Fund");

        Console.Write("\nOption: ");

        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("\nInvalid option.");
            Thread.Sleep(2000);
            return;
        }

        if (!Enum.IsDefined(typeof(InvestmentType), option))
        {
            Console.WriteLine("\nInvalid investment type.");
            Thread.Sleep(2000);
            return;
        }

        InvestmentType investmentType = (InvestmentType)option;
        decimal annualRate = GetAnnualRate(investmentType);

        Console.WriteLine("\nAmount to invest: R$ ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal InvestmentAmount))
        {
            Console.WriteLine("\nInvalid amount.");
            Thread.Sleep(3000);
            return;
        }
    }
    public void Redeem(BankAccount account)
    {
        throw new NotImplementedException();
    }
    public void ShowPortfolio(BankAccount account)
    {
        throw new NotImplementedException();
    }
    public decimal CalculateCurrentValue(Investment investment)
    {
        throw new NotImplementedException();
    }
    public void SimulateInvestment()
    {
        throw new NotImplementedException();
    }
}