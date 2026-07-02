using PrimeCapitalBank.Services;

namespace PrimeCapitalBank;

public class Program
{
    public static void Main(string[] args)
    {
        BankService bank = new BankService();
        bank.Start();
    }
}