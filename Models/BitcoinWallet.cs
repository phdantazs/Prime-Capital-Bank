using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;
public class BitcoinWallet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string WalletCode { get; set; } = string.Empty;

    public Guid BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; } = null!;

    public decimal Balance { get; set; }
    public BitcoinWalletStatus Status { get; set; } = BitcoinWalletStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }
    
    public List<BitcoinTransaction> Transactions { get; set; } = new();
}