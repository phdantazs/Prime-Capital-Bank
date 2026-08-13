using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Models;

public class BitcoinWallet
{
    public decimal Balance { get; set; }
    public List<BitcoinTransaction> Transactions { get; set; } = new();
}