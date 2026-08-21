using PrimeCapitalBank.Models;
using PrimeCapitalBank.Services.Core;
using PrimeCapitalBank.Models.Enums;
using System.Globalization;

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

        Console.WriteLine("\n============================================");
        Console.WriteLine("  \nBitcoin account created successfully!");
        Console.WriteLine("\n============================================");
        Console.WriteLine("\nYour Bitcoin wallet is now ready to use.");

        Thread.Sleep(3000);
    }

    private decimal GetBitcoinPrice()
    {
        return 600000m;
    }

    public void ShowBitcoinPrice()
    {
        decimal bitcoinPrice = GetBitcoinPrice();

        Console.Clear();

        Console.WriteLine("========== BITCOIN PRICE ==========\n");
        Console.WriteLine($"Current Bitcoin price: R$ {bitcoinPrice:N2}");
        Console.WriteLine("\n1 BTC = " + $"R$ {bitcoinPrice:N2}");
        Console.WriteLine("\nPrice shown for simulation purposes.");

        Console.WriteLine("\nPress ENTER to return.");
        Console.ReadLine();
    }

    public void BuyBitcoin(BankAccount account)
    {
        decimal bitcoinPrice = GetBitcoinPrice();

        Console.Clear();

        Console.WriteLine("========== BUY BITCOIN ==========\n");

        Console.WriteLine($"Current Bitcoin price: R$ {bitcoinPrice:N2}");
        Console.WriteLine($"\nAvailable account balance: R$ {account.Balance:N2}");

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
            Description = $"Purchase of {bitcoinAmount.ToString("F7", CultureInfo.InvariantCulture)} BTC",
            IsCredit = false
        });
    }

    public void SellBitcoin(BankAccount account)
    {
        decimal bitcoinPrice = GetBitcoinPrice();

        if (account.BitcoinWallet == null)
        {
            Console.WriteLine("\nBitcoin account not found.");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();

        Console.WriteLine("========== SELL BITCOIN ==========\n");
        Console.WriteLine($"Current Bitcoin price: R$ {bitcoinPrice:N2}");

       decimal bitcoinBalance = account.BitcoinWallet.Balance;
       decimal availableValue = Math.Round(bitcoinBalance * bitcoinPrice, 2);

       Console.WriteLine($"\nYour Bitcoin balance is: {bitcoinBalance.ToString("F7", CultureInfo.InvariantCulture)} BTC");
       Console.WriteLine($"\nAvailable value: R$ {availableValue:N2}");
       Console.WriteLine("\n==================================================");

       Console.WriteLine("\nHow do you want to sell?");

       Console.WriteLine("\n1 - Sell by Bitcoin amount");
       Console.WriteLine("\n2 - Sell by BRL amount");

       Console.Write("\nChoose an option: ");
       int option = _inputService.ReadMenuOption(1, 2);

       Console.WriteLine("\n=======================\n");

       decimal bitcoinAmount;
       decimal totalAmount;

       if (option == 1)
        {
            bitcoinAmount = _inputService.ReadBitcoinAmount("Amount of Bitcoin to sell: ");

            if (bitcoinAmount <= 0)
            {
                Console.WriteLine("\nInvalid Bitcoin amount.");
                Thread.Sleep(1500);
                return;
            }

            if (bitcoinAmount > bitcoinBalance)
            {
                Console.WriteLine("\nInsufficient Bitcoin balance.");
                Thread.Sleep(1500);
                return;
            }

            totalAmount = bitcoinAmount * bitcoinPrice;
            totalAmount = Math.Round(totalAmount, 2);
        }

        else
        {
            totalAmount = _inputService.ReadMoney("Amount in BRL to sell: ");

            if (totalAmount <= 0)
            {
                Console.WriteLine("\nInvalid amount.");
                Thread.Sleep(1500);
                return;
            }

            totalAmount = Math.Round(totalAmount, 2);

            if (totalAmount > availableValue || bitcoinBalance <= 0)
            {
                Console.WriteLine("\nInsufficient Bitcoin balance.");
                Thread.Sleep(1500);
                return;
            }

            if (totalAmount == availableValue)
            {
                bitcoinAmount = bitcoinBalance;
                totalAmount = Math.Round(bitcoinAmount * bitcoinPrice, 2);
            }

            else
            {
                bitcoinAmount = totalAmount / bitcoinPrice; 
            } 
        }

        Console.WriteLine("\n========== SALE SUMMARY ==========\n");

        Console.WriteLine($"Bitcoin amount: {bitcoinAmount.ToString("F7", CultureInfo.InvariantCulture)} BTC");
        Console.WriteLine($"\nBitcoin price: R$ {bitcoinPrice:N2}");
        Console.WriteLine($"\nAmount received: R$ {totalAmount:N2}");

        Console.Write("\nConfirm sale? (Y/N): ");

        string confirmation = Console.ReadLine()!.Trim().ToUpper();

        if (confirmation != "Y")
        {
            Console.WriteLine("\nBitcoin sale cancelled.");
            Thread.Sleep(1500);
            return;
        }

        account.BitcoinWallet.Balance -= bitcoinAmount;
        
        if (account.BitcoinWallet.Balance < 0.0000001m)
            account.BitcoinWallet.Balance = 0m;
        
        account.Balance += totalAmount;

        account.BitcoinWallet.Transactions.Add(new BitcoinTransaction
        {
            Type = BitcoinTransactionType.Sell,
            BitcoinAmount = bitcoinAmount,
            BitcoinPrice = bitcoinPrice,
            TotalAmount = totalAmount
        });

        account.Transactions.Add(new Transaction
        {
            Date = DateTime.Now,
            Type = "Bitcoin Sale",
            Amount = totalAmount,
            Description = $"Sale of {bitcoinAmount.ToString("F7", CultureInfo.InvariantCulture)} BTC",
            IsCredit = true
        });
        
        Console.WriteLine("\n==============================================");
        Console.WriteLine("\nBitcoin sold successfully!");
        Console.WriteLine($"\nBTC sold: {bitcoinAmount.ToString("F7", CultureInfo.InvariantCulture)}");

        Console.WriteLine($"\nAmount received: R$ {totalAmount:N2}");

        Thread.Sleep(5000);
    }

    public void ShowBitcoinWallet(BankAccount account)
    {
        if (account.BitcoinWallet == null)
        {
            Console.WriteLine("\nBitcoin account not found.");
            Thread.Sleep(1500);
            return;
        }

        decimal bitcoinPrice = GetBitcoinPrice();
        decimal bitcoinBalance = account.BitcoinWallet.Balance;
        decimal walletValue = bitcoinBalance * bitcoinPrice;

        decimal totalBought = account.BitcoinWallet.Transactions
            .Where(transaction => transaction.Type == BitcoinTransactionType.Buy)
            .Sum(transaction => transaction.TotalAmount);

        decimal totalSold = account.BitcoinWallet.Transactions
            .Where(transaction => transaction.Type == BitcoinTransactionType.Sell)
            .Sum(transaction => transaction.TotalAmount);

        decimal profitLoss = walletValue + totalSold - totalBought;

        Console.Clear();

        Console.WriteLine("========== MY BITCOIN WALLET ==========\n");

        Console.WriteLine($"Bitcoin balance: {bitcoinBalance.ToString("F7", CultureInfo.InvariantCulture)} BTC");
        Console.WriteLine($"Current Bitcoin price: R$ {bitcoinPrice:N2}");
        Console.WriteLine($"Wallet value: R$ {walletValue:N2}");
        Console.WriteLine("\n--------------------------------------");
        
        Console.WriteLine($"\nTotal bought: R$ {totalBought:N2}");
        Console.WriteLine($"Total sold: R$ {totalSold:N2}");
        Console.WriteLine($"Profit/Loss: R$ {profitLoss:N2}");
        Console.WriteLine("\n--------------------------------------");

        Console.WriteLine("\nPress ENTER to return.");

        Console.ReadLine();
    }

    public void ShowBitcoinTransactions(BankAccount account)
    {
        if (account.BitcoinWallet == null)
        {
            Console.WriteLine("\nBitcoin account not found.");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        Console.WriteLine("\n========== BITCOIN TRANSACTIONS ==========\n");

        if (account.BitcoinWallet.Transactions.Count == 0)
        {
            Console.WriteLine("No Bitcoin transactions found.");
            Console.WriteLine("\nPress ENTER to return.");
            Console.ReadLine();

            return;
        }

        foreach (BitcoinTransaction transaction in account.BitcoinWallet.Transactions
                    .OrderByDescending(transaction => transaction.CreatedAt))
        {
            Console.WriteLine($"Type: {transaction.Type}");
            Console.WriteLine($"BTC Amount: {transaction.BitcoinAmount.ToString("F7", CultureInfo.InvariantCulture)} BTC");
            Console.WriteLine($"Bitcoin Price: R$ {transaction.BitcoinPrice:N2}");
            Console.WriteLine($"Total Amount: R$ {transaction.TotalAmount:N2}");
            Console.WriteLine($"Date: {transaction.CreatedAt:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine("----------------------------------\n");
        }
            Console.WriteLine("Press ENTER to return.");
            Console.ReadLine();
    }

    public void CloseBitcoinAccount(BankAccount account)
    {
        if (account.BitcoinWallet == null)
        {
            Console.WriteLine("\nBitcoin account not found.");
            Thread.Sleep(1500);
            return;
        }

        if (account.BitcoinWallet.Balance > 0.0000001m)
        {
            Console.WriteLine("\nYou cannot close your Bitcoin account while you still have Bitcoin.");
            Console.WriteLine("\nPlease sell all your Bitcoin before closing the account.");
            Thread.Sleep(3500);

            return;
        }

        Console.Clear();

        Console.WriteLine("========== CLOSE BITCOIN ACCOUNT ==========\n");

        Console.WriteLine("Your Bitcoin wallet has no remaining balance.");
        Console.Write("\nAre you sure you want to close your Bitcoin account? (Y/N): ");

        string? confirmation = Console.ReadLine()?.Trim().ToUpper();

        if (confirmation != "Y")
        {
            Console.WriteLine("\nOperation cancelled.");
            Thread.Sleep(1500);
            return;
        }

        account.BitcoinWallet = null;
        
        Console.WriteLine("\nBitcoin account closed successfully!");

        Thread.Sleep(2000);
    }
}
