using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;

namespace PrimeCapitalBank.Services.Core;

public class AccountService
{
    public string GetAccountType(AccountType accountType)
    {
        return accountType switch
        {
            AccountType.Checking => "Checking Account",
            AccountType.Savings => "Savings Account",
            _ => throw new ArgumentOutOfRangeException(nameof(accountType), "Invalid account type.")
        };
    }

    public BankAccount CreateAccount(AccountType accountType)
    {
        return new BankAccount
        {
            AccountType = accountType,
            Balance = 0m,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Deposit(BankAccount account, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("\nThe deposit amount must be greater than zero.");
            Thread.Sleep(2000);
            return;
        }

        account.Balance += amount;

        account.Transactions.Add(new Transaction
        {
            BankAccountId = account.Id,
            BankAccount = account,
            Type = TransactionType.Deposit,
            Amount = amount,
            Description = "Cash deposit",
            IsCredit = true
        });

        Console.WriteLine("\nDeposit completed successfully!");
        Console.WriteLine($"\nYour actual balance is: R$ {account.Balance:N2}");

        Thread.Sleep(3000);
        Console.Clear();
    }

public void Withdraw(BankAccount account, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("\nThe withdrawal amount must be greater than zero.");
            Thread.Sleep(2000);
            return;
        }
    

    //VERIFICAR SE HÁ SALDO SUFICIENTE PARA O SAQUE
    if (amount > account.Balance)
    {
        Console.WriteLine("\nInsufficient balance.");
        Thread.Sleep(2000);
        return;
    }

    //SE TIVER SALDO SUFICIENTE, REALIZAR O SAQUE
    account.Balance -= amount;

     account.Transactions.Add(new Transaction
        {
            BankAccountId = account.Id,
            BankAccount = account,
            Type = TransactionType.Withdrawal,
            Amount = amount,
            Description = "Cash withdrawal",
            IsCredit = false
        });

    Console.WriteLine("\nWithdrawal completed successfully!");
    Console.WriteLine($"\nCurrent Balance: R$ {account.Balance:N2}");

    Thread.Sleep(3000);
    Console.Clear();
}

public void Transfer(BankAccount originAccount, BankAccount destinationAccount, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("\nThe transfer amount must be greater than zero.");
            Thread.Sleep(2000);
            return;
        }
        if (originAccount.Id == destinationAccount.Id)
        {
            Console.WriteLine("\nThe origin and destination accounts must be different.");
            Thread.Sleep(2000);
            return;
        }
        if (amount > originAccount.Balance)
        {
            Console.WriteLine("\nInsufficient balance.");
            Thread.Sleep(2000);
            return;
        }

        var transfer = new Transfer
        {
            SourceAccountId = originAccount.Id,
            SourceAccount = originAccount,
            DestinationAccountId = destinationAccount.Id,
            DestinationAccount = destinationAccount,
            Amount = amount
        };

        originAccount.Balance -= amount;
        destinationAccount.Balance += amount; 

        var debitTransaction = new Transaction
        {
          BankAccountId = originAccount.Id,
          BankAccount = originAccount,
          TransferId = transfer.Id,
          Transfer = transfer,
          Type = TransactionType.Transfer,
          Amount = amount,
          Description = $"Transfer to {destinationAccount.Owner.Name} ({destinationAccount.AccountNumber})",
          IsCredit = false  
        };

        var creditTransaction = new Transaction
        {
          BankAccountId = destinationAccount.Id,
          BankAccount = destinationAccount,
          TransferId = transfer.Id,
          Transfer = transfer,
          Type = TransactionType.Transfer,
          Amount = amount,
          Description = $"Transfer received from {originAccount.Owner.Name} ({originAccount.AccountNumber})",
          IsCredit = true 
        };

        originAccount.Transactions.Add(debitTransaction);
        destinationAccount.Transactions.Add(creditTransaction);

        transfer.Transactions.Add(debitTransaction);
        transfer.Transactions.Add(creditTransaction);

        Console.WriteLine("\nTransfer completed successfully!");
        Console.WriteLine($"\nTransferred amount: R$ {amount:N2}");
        Console.WriteLine($"\nCurrent balance: R$ {originAccount.Balance:N2}");

        Thread.Sleep(2500);
        Console.Clear();
    }

public void Statement(BankAccount account)
    {
        Console.Clear();

        Console.WriteLine("=========================");
        Console.WriteLine("    ACCOUNT STATEMENT");
        Console.WriteLine("=========================");

    Console.WriteLine($"\nAccount Number: {account.AccountNumber}");
    Console.WriteLine($"Account Type: {GetAccountType(account.AccountType)}");
    Console.WriteLine($"Created At: {account.CreatedAt:dd/MM/yyyy HH:mm}");
    Console.WriteLine($"\nCurrent Balance: R$ {account.Balance:N2}");

    Console.WriteLine();
    Console.WriteLine(new string('-', 125));
    Console.WriteLine("\nTransactions:\n");

    if (account.Transactions.Count == 0)
    {
        Console.WriteLine("No transactions found.");
    }
    else
    {
        foreach (Transaction transaction in account.Transactions)
        {
            string signal = transaction.IsCredit ? "+" : "-";

            Console.WriteLine(
                $"{transaction.CreatedAt:dd/MM/yyyy HH:mm} | " +
                $"{transaction.Type, -25} | " +
                $"{transaction.Description, -70} | " + 
                $"{signal} R$ {transaction.Amount,12:N2}");

                Console.WriteLine($"\nTransaction ID: {transaction.Id}\n");
        }
    }

    Console.WriteLine(new string('-', 125));
    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
    Console.Clear();
    }
}