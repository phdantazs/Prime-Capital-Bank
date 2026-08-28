namespace PrimeCapitalBank.Models;
public class Customer
{
    public int Id { get; set;}
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public decimal MonthlyIncome { get; set; }
    public string PinHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<BankAccount> Accounts {get; set; } = new();
    public List<InvestmentSimulation> Simulations { get; set; } = new();
}