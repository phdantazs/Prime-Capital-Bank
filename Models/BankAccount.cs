using PrimeCapitalBank.Models.Enums;
namespace PrimeCapitalBank.Models;

public class BankAccount
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CustomerId { get; set; }
    public Customer Owner { get; set;} = null!;
    public int FailedLoginAttempts { get; set; } = 0;
    public DateTime? BlockedUntil { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
    public List<Investment> Investments { get; set; } = new();
    public BitcoinWallet? BitcoinWallet { get; set;}
}