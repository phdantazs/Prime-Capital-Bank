using PrimeCapitalBank.Models;
using System;

namespace PrimeCapitalBank.Services;

public class AccountService
{
    private int _nextAccountNumber = 1001;
    public string GenerateAccountNumber(AccountType accountType)
    {
        string suffix = accountType == AccountType.Checking ? "C" : "P";
        string accountNumber = $"{_nextAccountNumber:D6}-{suffix}";
        _nextAccountNumber++;

        return accountNumber;
    }
    public string GetAccountType(AccountType accountType)
    {
        return accountType == AccountType.Checking
            ? "Checking Account"
            : "Savings Account";
    }

    public BankAccount CreateAccount(AccountType accountType)
    {
        return new BankAccount
        {
            AccountType = accountType,
            AccountNumber = GenerateAccountNumber(accountType),
            Balance = 0,
            CreatedAt = DateTime.Now
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
            Type = "Deposit",
            Amount = amount,
            Description = "Cash deposit",
            IsCredit = true
        });

        Console.WriteLine("\nDeposit completed sucessfully!");
        Console.WriteLine($"\nYour actual balance is: R$ {account.Balance:N2}");

        Thread.Sleep(2000);

        }

public void Withdraw(BankAccount account, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("\nThe withdrawal ammount must be greater than zero.");
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
            Type = "Withdrawal",
            Amount = amount,
            Description = "Cash withdrawal",
            IsCredit = false
        });

    Console.WriteLine("\nWithdrawal completed successfully!");
    Console.WriteLine($"\nCurrent Balance: R$ {account.Balance:N2}");

    Thread.Sleep(2500);
}

public void Transfer(BankAccount originAccount, BankAccount destinationAccount, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("\nThe transfer amount must be greater than zero.");
            Thread.Sleep(2000);
            return;
        }
        if (amount > originAccount.Balance)
        {
            Console.WriteLine("\nInsufficient balance.");
            Thread.Sleep(2000);
            return;
        }

        originAccount.Balance -= amount;
        destinationAccount.Balance += amount;

        originAccount.Transactions.Add(new Transaction
        {
            Type = "Transfer",
            Amount = amount,
            Description = $"Transfer to {destinationAccount.Owner.Name} ({destinationAccount.AccountNumber})",
            IsCredit = false
        });

        destinationAccount.Transactions.Add(new Transaction
        {
            Type = "Transfer",
            Amount = amount,
            Description = $"Transfer received from {originAccount.Owner.Name} ({originAccount.AccountNumber})",
            IsCredit = true
        });

        Console.WriteLine("\nTransfer completed sucessfully!");
        Console.WriteLine($"\nTransferred amount: R$ {amount:N2}");
        Console.WriteLine($"\nCurrent balance: R$ {originAccount.Balance:N2}");

        Thread.Sleep(2500);
    }

public void Statement(BankAccount account)
    {
        Console.Clear();

        Console.WriteLine("=========================");
        Console.WriteLine("ACCOUNT STATEMENT");
        Console.WriteLine("=========================");

    Console.WriteLine($"Account Number : {account.AccountNumber}");
    Console.WriteLine($"Account Type   : {account.AccountType}");
    Console.WriteLine($"Created At     : {account.CreatedAt:dd/MM/yyyy HH:mm}");
    Console.WriteLine($"Current Balance: R$ {account.Balance:N2}");

    Console.WriteLine("\nTransactions");
    Console.WriteLine("------------------------------------------------------------");

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
                $"{transaction.Date:dd/MM/yyyy HH:mm} | " +
                $"{signal} R$ {transaction.Amount,10:N2} | " +
                $"{transaction.Description}");
        }
    }

    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
}

}



