using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Services.Investments;

public class IncomeTaxService
{
    private readonly InvestmentService _investmentService;
    private readonly TaxService _taxService;
    public IncomeTaxService(
        InvestmentService investmentService,
        TaxService taxService)
    {
        _investmentService = investmentService;
        _taxService = taxService;
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
        
        decimal taxRate = _taxService.GetTaxRate(investment);
        decimal tax = 
            _taxService.CalculateIncomeTax(
                profit,
                investment);

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
}