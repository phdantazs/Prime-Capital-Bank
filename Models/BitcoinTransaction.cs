using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;

public class BitcoinTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int BankAccountId { get; set; }
    public BitcoinWallet BitcoinWallet { get; set; } = null!;
    public BitcoinTransactionType Type { get; set; }
    public decimal BitcoinAmount { get; set; }
    public decimal BitcoinPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}