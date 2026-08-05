using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Services.Investments;

public class IncomeTaxService
{
    private readonly InvestmentService _investmentService;
    public IncomeTaxService(InvestmentService investmentService)
    {
        _investmentService = investmentService;
    }
    public void Calculate(BankAccount account)
    {
        if (!account.Investments.Any())
        {
            Console.WriteLine("\nYou have no investments available.");
            Thread.Sleep(3000);
            return;
        }

        Console.Clear();

    Console.WriteLine("========== INCOME TAX CALCULATOR ==========\n");

    decimal totalTax = 0;
    decimal totalNetValue = 0;

    foreach (Investment investment in account.Investments)
    {
        decimal currentValue = _investmentService.CalculateCurrentValue(investment);
        decimal profit = currentValue - investment.InvestmentAmount;
        
        decimal taxRate = GetTaxRate(investment);
        decimal tax = profit > 0 ? profit * taxRate : 0;

        decimal netValue = currentValue - tax;

        totalTax += tax;
        totalNetValue += netValue;

        Console.WriteLine($"Investment: {investment.Type}");
        Console.WriteLine($"Invested amount: R$ {investment.InvestmentAmount:C}");
        Console.WriteLine($"Current value: {currentValue:C}");
        Console.WriteLine($"Profit: {profit:C}");
        Console.WriteLine($"Income tax rate: {taxRate:P1}");
        Console.WriteLine($"Income tax: {tax:C}");
        Console.WriteLine($"Net redemption value: {netValue:C}");
        Console.WriteLine("--------------------------------------\n");
    }

        Console.WriteLine("========== SUMMARY ==========");
        Console.WriteLine($"\nTotal income tax: {totalTax:C}");
        Console.WriteLine($"Total net redemption: {totalNetValue:C}");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    private decimal GetTaxRate(Investment investment)
    {
        //LCI e LCA são isentas de IR para pessoa física
        if (investment.Type == InvestmentType.LCI ||
            investment.Type == InvestmentType.LCA)
        {
        return 0m;
        }

        int days = (DateTime.Now - investment.InvestedAt).Days;
        //Valores com base na tabela regressiva de IR para renda fixa
        if (days <= 180)
            return 0.225m;

        if (days <= 360)
            return 0.20m;

        if (days <= 720)
            return 0.175m;

        return 0.15m;
    }    
}