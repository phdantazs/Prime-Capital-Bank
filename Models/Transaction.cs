using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;
public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; } = null!;
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCredit { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}