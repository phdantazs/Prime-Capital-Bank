namespace PrimeCapitalBank.Models;
public class Customer
{
    public string Name { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string IdNumber { get; set; } = "";
    public decimal MonthlyIncome { get; set; }
    public string Pin { get; set; } = "";
    public List<BankAccount> Accounts {get; set; } = new();
}