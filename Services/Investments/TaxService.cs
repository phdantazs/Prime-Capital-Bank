using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Services.Investments;

public class TaxService
{
    public decimal GetTaxRate(Investment investment)
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

    public decimal CalculateIncomeTax(
        decimal profit,
        Investment investment)
    {
        if (profit <= 0)
            return 0;

        return profit * GetTaxRate(investment);
    }
}