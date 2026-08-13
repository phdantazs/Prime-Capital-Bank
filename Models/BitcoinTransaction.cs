using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;

public class BitcoinTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public BitcoinTransactionType Type { get; set; }
    public decimal BitcoinAmount { get; set; }
    public decimal BitcoinPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}