using PrimeCapitalBank.Models;

namespace PrimeCapitalBank.Services;
public class AuthenticationService
{
    public string ReadPin()
    {
        string pin = "";

        while (pin.Length < 6)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
            {
                pin = pin[..^1];
                Console.Write("\b \b");
                continue;
            }

            if (char.IsDigit(key.KeyChar))
            {

                pin += key.KeyChar;
                Console.Write("*");
            }
        }

        Console.WriteLine();
        return pin;
    }

    public string CreatePin(string? currentPin = null)
    {
        while (true)
        {
            Console.WriteLine("\nCreate a 6-digit PIN: ");
            string pin = ReadPin();

            if (currentPin != null && pin == currentPin)
            {
                Console.WriteLine("\nThe new PIN cannot be the same as the current PIN.\n");
                continue;
            }

            Console.WriteLine("\nConfirm your PIN: ");
            string confirmPin = ReadPin();

            if (pin == confirmPin)
                return pin;

            Console.WriteLine("\nPINs do not match. Please try again.\n");
        }
    }

    public bool Authenticate(BankAccount account, string pin)
    {
        // Verifica se a conta está bloqueada
        if (account.BlockedUntil.HasValue)
    {
        if (DateTime.Now < account.BlockedUntil.Value)
        {
            TimeSpan remaining = account.BlockedUntil.Value - DateTime.Now;
            Console.WriteLine($"\nThis account is temporarily blocked. Try again in {remaining.Minutes:D2}:{remaining.Seconds:D2}.");

            return false;
        }

        // Unblock the account automatically
        account.BlockedUntil = null;
        account.FailedLoginAttempts = 0;
    }

    // Validate the PIN

    if (account.Pin != pin)
    {
        account.FailedLoginAttempts++;
        int remainingAttempts = 3 - account.FailedLoginAttempts;

        if (remainingAttempts > 0)
        {
            Console.WriteLine($"\nInvalid PIN. {remainingAttempts} attempt(s) remaining.");
        }
        else
        {
            account.BlockedUntil = DateTime.Now.AddMinutes(2);
            Console.WriteLine("\nYour account has been temporarily blocked for 2 minutes.");
        }

        return false;

    }

    // Successful login
    account.FailedLoginAttempts = 0;

    return true;
    }

    public void ChangePin(Customer customer)
    {
        Console.Write("\nEnter your current PIN: ");
        string currentPin = ReadPin();

        if (customer.Accounts.First().Pin != currentPin)
        {
            Console.WriteLine("\nCurrent PIN is incorrect.");
            return;
        }

        string newPin = CreatePin(currentPin);

        foreach (BankAccount account in customer.Accounts)
        {
            account.Pin = newPin;
        }

        Console.WriteLine("\nPIN changed successfully!");
    }
}
