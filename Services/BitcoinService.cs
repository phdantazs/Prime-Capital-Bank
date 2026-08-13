using PrimeCapitalBank.Models;
using PrimeCapitalBank.Services.Core;
using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Services.Bitcoin;

public class BitcoinService
{
    private readonly InputService _inputService;

    public BitcoinService(InputService inputService)
    {
        _inputService = inputService;
    }
    public void OpenBitcoinAccount(BankAccount account)
    {
        if (account.BitcoinWallet != null)
        {
            Console.WriteLine("\nYou already have a Bitcoin account.");
            Thread.Sleep(3000);
            return;
        }

        account.BitcoinWallet = new BitcoinWallet();

        Console.WriteLine("\n====================================");
        Console.WriteLine("Bitcoin account created successfully!");
        Console.WriteLine("======================================");
        Console.WriteLine("\nYour Bitcoin wallet is now ready to use.");

        Thread.Sleep(3000);
    }

    private decimal GetBitcoinPrice()
    {
        return 600000m;
    }

    public void BuyBitcoin(BankAccount account)
    {
        decimal bitcoinPrice = GetBitcoinPrice();

        Console.Clear();

        Console.WriteLine("========== BUY BITCOIN ==========\n");

        Console.WriteLine($"Current Bitcoin price: R$ {bitcoinPrice:N2}");

        decimal amount = _inputService.ReadMoney("\nAmount to invest: ");

        if (amount <= 0)
        {
            Console.WriteLine("\nInvalid amount.");
            Thread.Sleep(3000);
            return;
        }

        if (amount > account.Balance)
        {
            Console.WriteLine("\nInsufficient balance.");
            Thread.Sleep(3000);
            return;
        }

        decimal bitcoinAmount = amount / bitcoinPrice;
        bitcoinAmount = Math.Round(bitcoinAmount, 8);

        account.Balance -= amount;

        account.BitcoinWallet!.Balance += bitcoinAmount;

        account.BitcoinWallet.Transactions.Add(new BitcoinTransaction
        {
            Type = BitcoinTransactionType.Buy,
            BitcoinAmount = bitcoinAmount,
            BitcoinPrice = bitcoinPrice,
            TotalAmount = amount
        });

        account.Transactions.Add(new Transaction
        {
            Date = DateTime.Now,
            Type = "Bitcoin Purchase",
            Amount = amount,
            Description = $"Purchase of {bitcoinAmount:F8} BTC",
            IsCredit = false
        });
    }
}