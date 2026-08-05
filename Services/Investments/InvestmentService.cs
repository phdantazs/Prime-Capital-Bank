using System.Data.Common;
using System.Runtime.ExceptionServices;
using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;
namespace PrimeCapitalBank.Services.Investments;

public class InvestmentService
{
    private readonly InputService _inputService;
    public InvestmentService(InputService inputService)
    {
        _inputService = inputService;
    }
    private decimal GetAnnualRate(InvestmentType investmentType)
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
        InvestmentType investmentType = SelectInvestmentType();
        decimal annualRate = GetAnnualRate(investmentType);
        decimal investmentAmount = _inputService.ReadMoney("Amount to invest: R$ ");

        if (investmentAmount <= 0)
        {
            Console.WriteLine("\nInvalid amount.");
            Thread.Sleep(3000);
            return;
        }

        if (!HasSufficientBalance(account, investmentAmount))
        {
            Console.WriteLine("\nInsufficient balance.");
            Thread.Sleep(3000);
            return;
        }

        Investment investment = CreateInvestment(
            investmentType,
            investmentAmount,
            annualRate);

        account.Balance -= investmentAmount;
        account.Investments.Add(investment);

        Console.WriteLine("\nInvestment completed successfully!");
        Thread.Sleep(3000);
    }
    public void Redeem(BankAccount account)
    {
        if (!account.Investments.Any())
        {
            Console.WriteLine("\nYou have no investments to redeem.");
            Thread.Sleep(3000);
            return;
        }

        Console.Clear();

        Console.WriteLine("========== REDEEM INVESTMENT ==========");

        for (int i = 0; i < account.Investments.Count; i++)
        {
            Investment investment = account.Investments[i];

            decimal currentValue = CalculateCurrentValue(investment);
            decimal profit = currentValue - investment.InvestmentAmount;
            decimal profitPercentage = (profit / investment.InvestmentAmount) * 100;

            Console.WriteLine($"{i + 1} - {investment.Type}");
            Console.WriteLine($"Invested amount: R$ {investment.InvestmentAmount:C}");
            Console.WriteLine($"Current value: R$ {currentValue:C}");
            Console.WriteLine($"Profit: R$ {profit:C}");
            Console.WriteLine($"Gain: {profitPercentage:F2}%");
            Console.WriteLine($"Annual rate: {investment.AnnualRate:P2}");
            Console.WriteLine($"Invested at: {investment.InvestedAt:d}");
            Console.WriteLine();
        }

        Console.Write("Choose an investment to redeem: ");

        int option = _inputService.ReadMenuOption(1, account.Investments.Count);
        Investment selectedInvestment = account.Investments[option - 1];
        decimal redemptionValue = CalculateCurrentValue(selectedInvestment);

        Console.WriteLine($"\nSelected investment: {selectedInvestment.Type}");
        Console.WriteLine($"Redemption value: R$ {redemptionValue:C}");

        Console.Write("\nConfirm redemption? (Y/N): ");
        string confirmation = Console.ReadLine()!.ToUpper();

        if (confirmation != "Y")
        {
            Console.WriteLine("\nRedemption canceled. Your investments remains the same way.");
            Thread.Sleep(3000);
            return;
        }

        account.Balance += redemptionValue;
        account.Investments.Remove(selectedInvestment);

        Console.WriteLine("\nInvestment redeemed successfully!");
        Console.WriteLine($"Amount credited: R$ {redemptionValue:C}");

        Thread.Sleep(3000);
    }
    public void ShowPortfolio(BankAccount account)
    {
        if (!account.Investments.Any())
        {
            Console.WriteLine("\nYou have no investments available.");
            Thread.Sleep(3000);
            return;
        }

        Console.Clear();

        Console.WriteLine("========== MY PORTFOLIO ==========\n");

        decimal totalInvested = 0;
        decimal totalCurrentValue = 0;

        foreach (Investment investment in account.Investments)
        {
            decimal currentValue = CalculateCurrentValue(investment);
            decimal profit = currentValue - investment.InvestmentAmount;
            decimal profitPercentage = (profit / investment.InvestmentAmount) * 100;

            totalInvested += investment.InvestmentAmount;
            totalCurrentValue += currentValue;

            Console.WriteLine($"Investment: {investment.Type}");
            Console.WriteLine($"Invested amout: R$ {investment.InvestmentAmount:C}");
            Console.WriteLine($"Current value: R$ {currentValue:C}");
            Console.WriteLine($"Profit: R$ {profit:C}");
            Console.WriteLine($"Gain: {profitPercentage:F2}%");
            Console.WriteLine($"Annual rate: {investment.AnnualRate:P2}");
            Console.WriteLine($"Invested at: {investment.InvestedAt:d}");
            Console.WriteLine("----------------------------------------------------\n");
        }

        decimal totalProfit = totalCurrentValue - totalInvested;
        decimal totalProfitPercentage = (totalProfit / totalInvested) * 100;

        Console.WriteLine("========== SUMMARY ==========\n");
        Console.WriteLine($"Total invested: R$ {totalInvested:C}");
        Console.WriteLine($"Current portfolio value: R$ {totalCurrentValue:C}");
        Console.WriteLine($"Total profit: R$ {totalProfit:C}");
        Console.WriteLine($"Total gain: {totalProfitPercentage:F2}%");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    public decimal CalculateCurrentValue(Investment investment)
    {
        double years = (DateTime.Now - investment.InvestedAt).TotalDays / 365;

        decimal currentValue = investment.InvestmentAmount *
            (decimal)Math.Pow(
                (double)(1 + investment.AnnualRate),
                years);
            
        return Math.Round(currentValue, 2);
    }
    public void SimulateInvestment(BankAccount account)
    {
        Console.Clear();

        Console.WriteLine("========== INVESTMENT SIMULATOR ==========\n");

        InvestmentType investmentType = SelectInvestmentType();

        decimal annualRate = GetAnnualRate(investmentType);
        decimal amount = _inputService.ReadMoney("\nInitial investment amount: ");

        if (amount <= 0)
        {
            Console.WriteLine("\nInvalid investment amount.");
            Thread.Sleep(3000);
            return;
        }

        Console.Write("\nInvestment period (years): ");
        int years = int.Parse(Console.ReadLine()!);

        if (years <= 0)
        {
            Console.WriteLine("\nInvalid period.");
            Thread.Sleep(3000);
            return;
        }

        decimal finalValue = amount *
            (decimal)Math.Pow(
                (double)(1 + annualRate),
                years);
            
        decimal profit = finalValue - amount;

        Console.WriteLine("\n========== SIMULATION RESULT ==========");
        Console.WriteLine($"\nInvestment: {investmentType}");
        Console.WriteLine($"Inital amount: R$ {amount:C}");
        Console.WriteLine($"Period: {years} years");
        Console.WriteLine($"Annual rate: {annualRate:P2}");
        Console.WriteLine($"Final value: {finalValue:C}");
        Console.WriteLine($"Profit: {profit:C}");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    private InvestmentType SelectInvestmentType()
    {
        Console.Clear();

        Console.WriteLine("========== INVESTMENTS ==========\n");
        Console.WriteLine("Choose an investment type:\n");

        Console.WriteLine("1 - Treasury Selic");
        Console.WriteLine("2 - CDB");
        Console.WriteLine("3 - LCI");
        Console.WriteLine("4 - LCA");
        Console.WriteLine("5 - Fixed Income Fund");

        Console.Write("\nChoose an option: ");

        int option = _inputService.ReadMenuOption(1, 5);

        return (InvestmentType)option;
    }
     private bool HasSufficientBalance(BankAccount account, decimal amount)
    {
       return account.Balance >= amount;
    }
    private Investment CreateInvestment(
        InvestmentType type,
        decimal amount,
        decimal annualRate)
    {
       return new Investment
       {
           Type = type,
           InvestmentAmount = amount,
           AnnualRate = annualRate,
           InvestedAt = DateTime.Now
       }; 
    }

}