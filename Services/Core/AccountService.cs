using PrimeCapitalBank.Models;

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
}