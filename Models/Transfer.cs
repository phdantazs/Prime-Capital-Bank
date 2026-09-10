namespace PrimeCapitalBank.Models;

public class Transfer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TransferCode { get; set; } = string.Empty;

    public Guid SourceAccountId { get; set; }
    public BankAccount SourceAccount { get; set; } = null!;

    public Guid DestinationAccountId { get; set; }
    public BankAccount DestinationAccount { get; set; } = null!;

    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Transaction> Transactions { get; set; } = new();
}