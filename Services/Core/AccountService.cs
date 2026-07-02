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
}