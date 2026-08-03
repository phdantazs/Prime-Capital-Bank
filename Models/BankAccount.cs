namespace PrimeCapitalBank.Models;

public class BankAccount
{
    public string AccountNumber { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
    public Customer Owner { get; set;} = null!;
    public List<Investment> Investments { get; set; } = new();

    // Segurança da conta
    public string Pin { get; set; } = string.Empty;
    public int FailedLoginAttempts { get; set; } = 0;
    public DateTime? BlockedUntil { get; set; }

}