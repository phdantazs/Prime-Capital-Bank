using System.Data.Common;
using System.Runtime.ExceptionServices;
using System.Linq;
using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace PrimeCapitalBank.Services.Investments;

public class InvestmentService
{
    private readonly InputService _inputService;
    private readonly TaxService _taxService;
    public InvestmentService(
        InputService inputService,
        TaxService taxService)
    {
        _inputService = inputService;
        _taxService = taxService;
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

        Console.WriteLine($"\nAvailable account balance: R$ {account.Balance:N2}");
        decimal investmentAmount = _inputService.ReadMoney("\nAmount to invest: ");

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

        AddTransaction(
            account,
            "Investment",
            investmentAmount,
            $"Investment {investment.Type}",
            false);

        Console.WriteLine("\nInvestment completed successfully!");
        Thread.Sleep(3000);
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
            RemainingAmount = amount,
            CurrentValue = amount,
            AnnualRate = annualRate,
            InvestedAt = DateTime.Now.AddDays(-457)
        };
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

        Console.WriteLine("========== REDEEM INVESTMENT ==========\n");

        for (int i = 0; i < account.Investments.Count; i++)
        {
            Investment investment = account.Investments[i];

            decimal currentValue = CalculateCurrentValue(investment);

            investment.CurrentValue = currentValue;

            decimal profit = currentValue - investment.RemainingAmount;

            decimal profitPercentage =
                investment.RemainingAmount > 0
                    ? (profit / investment.RemainingAmount) * 100
                    : 0;

            Console.WriteLine($"{i + 1} - {investment.Type}");
            Console.WriteLine($"\nInvested amount: {investment.InvestmentAmount:C}");
            Console.WriteLine($"Current value: {currentValue:C}");
            Console.WriteLine($"Profit: {profit:C}");
            Console.WriteLine($"Return: {profitPercentage:F2}%");
            Console.WriteLine($"Annual rate: {investment.AnnualRate:P2}");
            Console.WriteLine($"Invested since: {investment.InvestedAt:d}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }

        Console.Write("Choose an investment to redeem: ");

        int option = _inputService.ReadMenuOption(1, account.Investments.Count);
        Investment selectedInvestment = account.Investments[option - 1];

        //Atualiza o valor atual do investimento
        selectedInvestment.CurrentValue = CalculateCurrentValue(selectedInvestment);

        decimal availableAmount = selectedInvestment.CurrentValue;

        decimal currentProfit = selectedInvestment.CurrentValue - selectedInvestment.RemainingAmount;

        decimal profitabilityPercentage = 
            selectedInvestment.RemainingAmount > 0
                ? (currentProfit / selectedInvestment.RemainingAmount) * 100
                : 0;

        Console.Clear();
        
        Console.WriteLine("========== SELECTED INVESTMENT ==========\n");

        Console.WriteLine($"Investment: {selectedInvestment.Type}");
        Console.WriteLine($"Initial investment: {selectedInvestment.InvestmentAmount:C}");
        Console.WriteLine($"Current invested amount: {selectedInvestment.RemainingAmount:C}");
        Console.WriteLine($"Current value: {selectedInvestment.CurrentValue:C}");
        Console.WriteLine($"Profit: {currentProfit:C}");
        Console.WriteLine($"Return: {profitabilityPercentage:F2}");
        Console.WriteLine($"Annual rate: {selectedInvestment.AnnualRate:P2}");
        Console.WriteLine($"Invested since: {selectedInvestment.InvestedAt: dd/MM/yyyy}");

        Console.WriteLine($"\nAvailable redemption: {availableAmount:N2}");

        decimal redemptionValue =
            _inputService.ReadMoney("\nAmount to redeem: ");

        if (redemptionValue <= 0 || redemptionValue > availableAmount)
        {
            Console.WriteLine("\nInvalid redemption amount.");
            Thread.Sleep(3000);
            return;
        }

        decimal redemptionPercentage = redemptionValue / availableAmount;

        decimal redeemedPrincipal = selectedInvestment.RemainingAmount * redemptionPercentage;

        decimal redeemedProfit = redemptionValue - redeemedPrincipal;

        decimal taxRate = _taxService.GetTaxRate(selectedInvestment);

        decimal incomeTax =
            _taxService.CalculateIncomeTax(
                redeemedProfit,
                selectedInvestment);

        decimal netRedemption = redemptionValue - incomeTax;

        Console.WriteLine("\n========== REDEMPTION SUMMARY ==========\n");

        Console.WriteLine($"Requeseted redemption: {redemptionValue:C}");
        Console.WriteLine($"Principal redeemed: {redeemedPrincipal:C}");
        Console.WriteLine($"Profit redeemed: {redeemedProfit:C}");
        Console.WriteLine($"Income tax rate: {taxRate:P1}");
        Console.WriteLine($"Income tax: {incomeTax:C}");
        Console.WriteLine($"Net amount to be credited: {netRedemption:C}");

        Console.Write("\nConfirm redemption? (Y/N): ");
        string confirmation = Console.ReadLine()!.Trim().ToUpper();

        if (confirmation != "Y")
        {
            Console.WriteLine("\nRedemption canceled. Your investments remains unchanged.");
            Thread.Sleep(3000);
            return;
        }

        account.Balance += netRedemption;

        Transaction redemptionTransaction = new Transaction
        {
            Date = DateTime.Now,
            Type = "Investment Redemption",
            Amount = netRedemption,
            Description = $"Investment Redemption of {selectedInvestment.Type} (Taxes already deducted).",
            IsCredit = true
        };

        account.Transactions.Add(redemptionTransaction);

        selectedInvestment.CurrentValue -= redemptionValue;
        selectedInvestment.RemainingAmount -= redeemedPrincipal;

        if (selectedInvestment.CurrentValue < 0.01m)
            selectedInvestment.CurrentValue = 0;

        if (selectedInvestment.RemainingAmount < 0.01m)
            selectedInvestment.RemainingAmount = 0;

        if (selectedInvestment.CurrentValue == 0)
        {
            account.Investments.Remove(selectedInvestment);
        }
        
        Console.WriteLine("\n======================================");
        Console.WriteLine("\nInvestment redeemed successfully!");
        Console.WriteLine($"\nGross redemption: {redemptionValue:C}");
        Console.WriteLine($"Income tax: {incomeTax:C}");
        Console.WriteLine($"Net amount credited: R$ {netRedemption:C}");
        Console.WriteLine("\n======================================");

        Thread.Sleep(7000);
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
        decimal totalProfit = 0;

        foreach (Investment investment in account.Investments)
        {
            investment.CurrentValue = CalculateCurrentValue(investment);

            decimal profit = investment.CurrentValue - investment.RemainingAmount;

            decimal profitability = 
                investment.RemainingAmount > 0
                    ? (profit / investment.RemainingAmount) * 100
                    : 0;

            totalInvested += investment.RemainingAmount;
            totalCurrentValue += investment.CurrentValue;
            totalProfit += profit;

            Console.WriteLine($"Investment: {investment.Type}");
            Console.WriteLine($"Initial investment: {investment.InvestmentAmount:C}");
            Console.WriteLine($"Current invested amount: {investment.RemainingAmount:C}");
            Console.WriteLine($"Current value: {investment.CurrentValue:C}");
            Console.WriteLine($"Profit: {profit:C}");
            Console.WriteLine($"Return: {profitability:F2}%");
            Console.WriteLine($"Annual rate: {investment.AnnualRate:P2}");
            Console.WriteLine($"Invested since: {investment.InvestedAt:dd/MM/yyyy}");
            Console.WriteLine("\n---------------------------------------\n");
        }

        decimal totalProfitPercentage = 
            totalInvested > 0
                ? (totalProfit / totalInvested) * 100
                : 0;

        Console.WriteLine("========== SUMMARY ==========\n");
        Console.WriteLine($"Total invested: {totalInvested:C}");
        Console.WriteLine($"Current portfolio value: {totalCurrentValue:C}");
        Console.WriteLine($"Total profit: {totalProfit:C}");
        Console.WriteLine($"Total return: {totalProfitPercentage:F2}%");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    public decimal CalculateCurrentValue(Investment investment)
    {
        int days = (DateTime.Now - investment.InvestedAt).Days;

        decimal dailyRate =
            investment.AnnualRate / 365m;

        decimal currentValue =
            investment.RemainingAmount *
            (decimal)Math.Pow(
                (double)(1 + dailyRate),
                days);
            
        return Math.Round(currentValue, 2);
    }
    public void SimulateInvestment(BankAccount account)
    {
        Console.Clear();

        Console.WriteLine("========== INVESTMENT SIMULATOR ==========\n");

        SimulationResult result = BuildSimulation();

        result = CalculateSimulation(result);

        DisplaySimulation(result);

        InvestorProfile profile = AskInvestorProfile();

        List<SimulationResult> comparison = CompareInvestments(result);

        DisplayComparison(comparison);
        
        DisplayRecommendation(comparison, result, profile);

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

    private SimulationResult BuildSimulation()
    {
        InvestmentType investmentType = SelectInvestmentType();

        decimal annualRate = GetAnnualRate(investmentType);
        decimal initialInvestment =
            _inputService.ReadMoney("\nInitial investment amount: ");

        Console.WriteLine("\nRecurring contributions:");

        Console.WriteLine("\n1 - None");
        Console.WriteLine("2 - Monthly");
        Console.WriteLine("3 - Quarterly");
        Console.WriteLine("4 - Semi-Annual");
        Console.WriteLine("5 - Annual");

        Console.Write("\nChoose an Option: ");

        int frequencyOption = _inputService.ReadMenuOption(1, 5);

        ContributionFrequency contributionFrequency =
            frequencyOption switch
            {
                1 => ContributionFrequency.None,
                2 => ContributionFrequency.Monthly,
                3 => ContributionFrequency.Quarterly,
                4 => ContributionFrequency.SemiAnnual,
                5 => ContributionFrequency.Annual,
                _ => ContributionFrequency.None
            };

        decimal contributionAmount = 0;

        if (contributionFrequency != ContributionFrequency.None)
        {
            contributionAmount = 
                _inputService.ReadMoney("\nContribution amount: ");
        }

        Console.Write("\nInvestment period (1-30 years): ");

        int years = _inputService.ReadMenuOption(1, 30);

        return new SimulationResult
        {
            InvestmentType = investmentType,
            AnnualRate = annualRate,
            InitialInvestment = initialInvestment,
            ContributionAmount = contributionAmount,
            ContributionFrequency = contributionFrequency,
            Years = years
        };
    }

    private SimulationResult CalculateSimulation(SimulationResult result)
    {
        decimal balance = result.InitialInvestment;
        decimal totalContributed = result.InitialInvestment;

        int totalMonths = result.Years * 12;
        decimal monthlyRate = result.AnnualRate / 12;

        //Armazena todo aporte e o mês que o aporte entrou na simulação do investimento.
        List<(decimal Amount, int MonthInvested)> contributions = new();

        if (result.InitialInvestment > 0)
        {
            contributions.Add(
                (result.InitialInvestment, 0)
            );
        }

        for (int month = 1; month <= totalMonths; month++)
        {
            balance *= (1 + monthlyRate);

            bool shouldContribute = 
                result.ContributionFrequency switch
                {
                    ContributionFrequency.Monthly => true,
                    ContributionFrequency.Quarterly => month % 3 == 0,
                    ContributionFrequency.SemiAnnual => month % 6 == 0,
                    ContributionFrequency.Annual => month % 12 == 0,
                    _ => false
                };

            if (shouldContribute && result.ContributionAmount > 0)
            {
                balance += result.ContributionAmount;
                totalContributed += result.ContributionAmount;

                contributions.Add(
                    (result.ContributionAmount, month)
                );
            }
        }

        decimal grossProfit = balance - totalContributed;

        decimal incomeTax = 0m;

        //LCI e LCA são isentos de IR
        if (result.InvestmentType != InvestmentType.LCI && result.InvestmentType != InvestmentType.LCA)
        {
            foreach (var contribution in contributions)
            {
                int monthsInvested = totalMonths - contribution.MonthInvested;

                int daysInvested = monthsInvested * 30;

                decimal contributionFutureValue =
                    contribution.Amount *
                    CalculateCompoundGrowth(monthlyRate, monthsInvested);

                decimal contribuitonProfit = contributionFutureValue - contribution.Amount;

                if (contribuitonProfit <= 0)
                    continue;

                decimal taxRate;

                if (daysInvested <= 180)
                {
                    taxRate = 0.225m;
                }
                else if (daysInvested <= 360)
                {
                    taxRate = 0.20m;
                }
                else if (daysInvested <= 720)
                {
                    taxRate = 0.175m;
                }
                else
                {
                    taxRate = 0.15m;
                }

                incomeTax += contribuitonProfit * taxRate;
            }
        }

        decimal netProfit = grossProfit - incomeTax;
        decimal finalBalance = totalContributed + netProfit;

        result.TotalContributed = Math.Round(totalContributed, 2);
        result.FinalBalance = Math.Round(finalBalance, 2);
        result.InterestEarned = Math.Round(netProfit, 2);

        return result;
    }

    private decimal CalculateCompoundGrowth(
        decimal monthlyRate,
        int months)
    {
        if (months <= 0)
            return 1m;

        return (decimal)Math.Pow(
            (double)(1 + monthlyRate),
            months);
    }

    private void DisplaySimulation(SimulationResult result)
    {
        Console.Clear();

        InvestmentInfo info = GetInvestmentInfo(result.InvestmentType);

        Console.WriteLine("========== INVESTMENT SIMULATION ==========\n");

        Console.WriteLine($"Investment: {result.InvestmentType}");
        Console.WriteLine($"Initial investment: {result.InitialInvestment:C}");

        Console.WriteLine();

        Console.WriteLine($"Annual rate: {result.AnnualRate:P2}");
        Console.WriteLine($"Investment period: {result.Years} year(s)");
        Console.WriteLine();

        Console.WriteLine("============ INVESTMENT PROFILE ============\n");

        Console.WriteLine($"Risk Level: {info.RiskLevel}");
        Console.WriteLine($"Liquidity: {info.Liquidity}");
        Console.WriteLine($"Taxation: {info.Taxation}");
        Console.WriteLine($"FGC Protection: {info.FgcProtection}");
        Console.WriteLine($"Recommended Profile: {info.RecommendedProfile}");

        if (result.ContributionFrequency != ContributionFrequency.None)
        {
            Console.WriteLine($"Contribution frequency: {result.ContributionFrequency}");
            Console.WriteLine($"Contribution amount: {result.ContributionAmount:C}");
        }
        else
        {
            Console.WriteLine("Recurring contributions: None");
        }

        Console.WriteLine();

        Console.WriteLine("=============== RESULTS ===============\n");
        Console.WriteLine($"Total contributed: {result.TotalContributed:C}");
        Console.WriteLine($"Interest earned: {result.InterestEarned:C}");
        Console.WriteLine($"Final balance: {result.FinalBalance:C}");

        decimal profitability =
            result.TotalContributed > 0
                ? (result.InterestEarned / result.TotalContributed) * 100
                : 0;

        Console.WriteLine($"Profitability: {profitability:F2}%");
    }

    private List<SimulationResult> CompareInvestments(SimulationResult baseSimulation)
    {
        List<SimulationResult> results = new();

        foreach (InvestmentType investmentType in Enum.GetValues<InvestmentType>())
        {
            SimulationResult simulation = new()
            {
                InvestmentType = investmentType,
                InitialInvestment = baseSimulation.InitialInvestment,
                ContributionAmount = baseSimulation.ContributionAmount,
                ContributionFrequency = baseSimulation.ContributionFrequency,
                Years = baseSimulation.Years,
                AnnualRate = GetAnnualRate(investmentType)
            };

            results.Add(CalculateSimulation(simulation));
        }

        return results
            .OrderByDescending(r => r.FinalBalance)
            .ToList();
    }

    private InvestmentInfo GetInvestmentInfo(InvestmentType investmentType)
    {
        return investmentType switch
        {
            InvestmentType.TreasurySelic => new InvestmentInfo
            {
                RiskLevel = "Very Low",
                Liquidity = "Daily",
                Taxation = "Income Tax (Regressive Table)",
                FgcProtection = "No - Federal Government Guarantee",
                RecommendedProfile = "Conservative"
            },

            InvestmentType.CDB => new InvestmentInfo
            {
                RiskLevel = "Low",
                Liquidity = "Depends on the contract",
                Taxation = "Income Tax (Regressive Table)",
                FgcProtection = "Yes - FGC Protection",
                RecommendedProfile = "Conservative"
            },

            InvestmentType.LCI => new InvestmentInfo
            {
                RiskLevel = "Low",
                Liquidity = "At maturity",
                Taxation = "Tax Exempt",
                FgcProtection = "Yes - FGC Protection",
                RecommendedProfile = "Conservative"
            },

            InvestmentType.LCA => new InvestmentInfo
            {
                 RiskLevel = "Low",
                Liquidity = "At maturity",
                Taxation = "Tax Exempt",
                FgcProtection = "Yes - FGC Protection",
                RecommendedProfile = "Conservative"
            },

            InvestmentType.FixedIncomeFund => new InvestmentInfo
            {
                RiskLevel = "Low to Moderate",
                Liquidity = "Depends on the fund",
                Taxation = "Income Tax (Regressive Table)",
                FgcProtection = "No",
                RecommendedProfile = "Moderate"
            },

            _ => throw new ArgumentOutOfRangeException(nameof(investmentType))
        };
    }

    private void DisplayComparison(List<SimulationResult> results)
    {
        Console.WriteLine();

        Console.WriteLine("========== INVESTMENT COMPARISON ==========\n");

        const int rankWidth = 8;
        const int investmentWidth = 25;
        const int balanceWidth = 20;
        const int profitWidth = 20;

        Console.WriteLine(
            $"{"Rank", -rankWidth}" +
            $"{"Investment", -investmentWidth}" +
            $"{"Final Balance", -balanceWidth}" +
            $"{"Profit", -profitWidth}"
        );

        int tableWidth =
            rankWidth +
            investmentWidth +
            balanceWidth +
            profitWidth;
            
        Console.WriteLine(new string('-', tableWidth));

        int position = 1;

        foreach (SimulationResult result in results)
        {
            string medal = position switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => $"{position}º" 
            };

            string finalBalance = result.FinalBalance.ToString("C");
            string profit = result.InterestEarned.ToString("C");

            Console.WriteLine(
                $"{medal, -rankWidth}" +
                $"{result.InvestmentType, -investmentWidth}" +
                $"{finalBalance, -balanceWidth}" +
                $"{profit, -profitWidth}"
            );
            
            position++;
        }
    }

    private InvestorProfile AskInvestorProfile()
    {
        Console.WriteLine();

        Console.WriteLine("========== INVESTOR PROFILE ==========\n");

        Console.WriteLine("What is your main objective?");
        Console.WriteLine();

        Console.WriteLine("1 - I need daily liquidity for emergencies");
        Console.WriteLine("2 - I prefer security and stability");
        Console.WriteLine("3 - I accept some restrictions for better returns");
        Console.WriteLine("4 - I want the highest possible return");

        Console.Write("\nChoose an option: ");

        int option = _inputService.ReadMenuOption(1, 4);

        return (InvestorProfile)option;
    }

    private string GetRecommendationReason(
        SimulationResult investment,
        InvestorProfile profile)
    {
        return (profile, investment.InvestmentType) switch
        {
            //Liquidez diária
            (InvestorProfile.EmergencyLiquidity, InvestmentType.TreasurySelic) =>
                "Treasury Selic was recommended because it offers daily liquidity and very low risk, making it suitable for an emergency reserve.",

            //Conservador
            (InvestorProfile.Conservative, InvestmentType.TreasurySelic) =>
                "Treasury Selic was recommended because it combines very low risk, daily liquidity and the security of being issued by the Federal Government.",

            (InvestorProfile.Conservative, InvestmentType.CDB) =>
                "CDB was recommended because it offers low risk, FGC protection and competitive returns while maintaining a conservative risk profile.",
            
            (InvestorProfile.Conservative, InvestmentType.LCI) =>
                "LCI was recommended because it offers low risk, FGC protection and tax-exempt returns, making it suitable for a conservative investor.",

            (InvestorProfile.Conservative, InvestmentType.LCA) =>
                "LCA was recommended because it offers low risk, FGC protection and tax-exempt returns, making it suitable for a conservative investor.",

            //Equilibrado
            (InvestorProfile.Balanced, InvestmentType.TreasurySelic) =>
                "Treasury Selic was recommended because it provides a strong combination of security, liquidity and predictable returns for a balanced investor.",

            (InvestorProfile.Balanced, InvestmentType.CDB) =>
                "CDB was recommended because it provides a balance between profitability, relatively low risk and FGC protection.",

            (InvestorProfile.Balanced, InvestmentType.LCI) =>
                "LCI was recommended because it combines low risk, FGC protection and tax-exempt returns, providing a balanced investment option.",

            (InvestorProfile.Balanced, InvestmentType.LCA) =>
                "LCA was recommended because it combines low risk, FGC protection and tax-exempt returns, providing a balanced investment option.",

            (InvestorProfile.Balanced, InvestmentType.FixedIncomeFund) =>
                "The Fixed Income Fund was recommended because it provides diversification and the potential for competitive returns with a low to moderate level of risk.",

            // Máximo retorno
            (InvestorProfile.MaximumReturn, InvestmentType.TreasurySelic) =>
                "Treasury Selic was recommended because it achieved the highest projected net return in this simulation while maintaining very low risk.",

            (InvestorProfile.MaximumReturn, InvestmentType.CDB) =>
                "CDB was recommended because it achieved the highest projected net return in this simulation while still offering FGC protection.",

            (InvestorProfile.MaximumReturn, InvestmentType.LCI) =>
                "LCI was recommended because it achieved the highest projected net return in this simulation while offering tax-exempt returns and FGC protection.",

            (InvestorProfile.MaximumReturn, InvestmentType.LCA) =>
                "LCA was recommended because it achieved the highest projected net return in this simulation while offering tax-exempt returns and FGC protection.",

            (InvestorProfile.MaximumReturn, InvestmentType.FixedIncomeFund) =>
                "The Fixed Income Fund was recommended because it achieved the highest projected net return in this simulation. However, its risk level is higher than the most conservative alternatives.",

            _ =>
                "This investment was recommended based on the simulation results and your investor profile."
        };
    }

    private void DisplayRecommendation(
        List<SimulationResult> results,
        SimulationResult selectedInvestment,
        InvestorProfile profile)
    {
        SimulationResult bestInvestment = profile switch
        {
            InvestorProfile.EmergencyLiquidity =>
                results.First(r => r.InvestmentType == InvestmentType.TreasurySelic),

            InvestorProfile.Conservative =>
                results
                    .Where(r =>
                        r.InvestmentType == InvestmentType.TreasurySelic ||
                        r.InvestmentType == InvestmentType.CDB ||
                        r.InvestmentType == InvestmentType.LCI ||
                        r.InvestmentType == InvestmentType.LCA)
                    .OrderByDescending(r => r.FinalBalance)
                    .First(),

            InvestorProfile.Balanced =>
                results.First(),

            InvestorProfile.MaximumReturn =>
                results.First(),

            _ =>
                results.First()
        };

        string reason = GetRecommendationReason(
        bestInvestment,
        profile);

        Console.WriteLine();
        Console.WriteLine("========== RECOMMENDATION ==========\n");
        
        Console.WriteLine($"🏆 {bestInvestment.InvestmentType}");
        Console.WriteLine();

        Console.WriteLine("Why this investment:");

        Console.WriteLine($"\n{reason}");
        Console.WriteLine();

        Console.WriteLine($"Projected final balance: {bestInvestment.FinalBalance:C}");
        Console.WriteLine($"Projected profit: {bestInvestment.InterestEarned:C}");

        decimal difference = 
            bestInvestment.FinalBalance - selectedInvestment.FinalBalance;

        if (difference > 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"Difference compared to your selected investment: + {difference:C}");
        }
        else if (difference < 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"Difference compared to your selected investment: - {Math.Abs(difference):C}");  
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine(
                "Your selected investment has the highest projected return.");
        }
    }   

    private void AddTransaction(
        BankAccount account,
        string type,
        decimal amount,
        string description,
        bool isCredit)
    {
        account.Transactions.Add(new Transaction
        {
            Date = DateTime.Now,
            Type = type,
            Amount = amount,
            Description = description,
            IsCredit = isCredit
        });
    }
}