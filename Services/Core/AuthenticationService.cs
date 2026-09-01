using Microsoft.AspNetCore.Identity;
using PrimeCapitalBank.Models;

namespace PrimeCapitalBank.Services.Core;
public class AuthenticationService

{
    private readonly InputService _inputService;
    private readonly PasswordHasher<Customer> _passwordHasher;
    public AuthenticationService(InputService inputService)
    {   
        _inputService = inputService;
        _passwordHasher = new PasswordHasher<Customer>();
    }
    public string CreatePin(string? currentPin = null)
    {
        while (true)
        {
            Console.WriteLine("Create a 6-digit PIN: ");
            string pin = _inputService.ReadPin();

            if (currentPin != null && pin == currentPin)
            {
                Console.WriteLine("\nThe new PIN cannot be the same as the current PIN.\n");
                continue;
            }

            Console.WriteLine("\nConfirm your PIN: ");
            string confirmPin = _inputService.ReadPin();

            if (pin == confirmPin)
                return pin;

            Console.WriteLine("\nPINs do not match. Please try again.\n");
        }
    }

    public string HashPin(Customer customer, string pin)
    {
        return _passwordHasher.HashPassword(customer, pin);
    }

    public bool Authenticate(BankAccount account, string pin)
    {
        // Verifica se a conta está bloqueada
        if (account.BlockedUntil.HasValue)
    {
        if (DateTime.UtcNow < account.BlockedUntil.Value)
        {
            TimeSpan remaining = account.BlockedUntil.Value - DateTime.UtcNow;

            Console.WriteLine($"\nThis account is temporarily blocked. Try again in {remaining.Minutes:D2}:{remaining.Seconds:D2}.");

            return false;
        }

        // Unblock the account automatically
        account.BlockedUntil = null;
        account.FailedLoginAttempts = 0;
    }

    PasswordVerificationResult result = 
        _passwordHasher.VerifyHashedPassword(
                account.Owner,
                account.Owner.PinHash,
                pin);

    // Validate the PIN

    if (result == PasswordVerificationResult.Failed)
    {
        account.FailedLoginAttempts++;
        
        if (account.FailedLoginAttempts >= 3)
            {
                account.BlockedUntil = DateTime.UtcNow.AddMinutes(2);
                Console.WriteLine("\nYour account has been temporarily blocked for 2 minutes.");
            }

            else
            {
                int remainingAttempts = 3 - account.FailedLoginAttempts;

                Console.WriteLine($"\nInvalid PIN, {remainingAttempts} attempt(s) remaining");
            }

        return false;
    }

    account.FailedLoginAttempts = 0;
    account.BlockedUntil = null;

    return true;
    }

    public void ChangePin(Customer customer)
    {
        Console.Write("Enter your current PIN: ");
        string currentPin = _inputService.ReadPin();

        Console.WriteLine();

        PasswordVerificationResult result =
            _passwordHasher.VerifyHashedPassword(
                customer,
                customer.PinHash,
                currentPin);

        if (result == PasswordVerificationResult.Failed)
        {
            Console.WriteLine("\nCurrent PIN is incorrect.");
            return;
        }

        string newPin = CreatePin(currentPin);

        customer.PinHash = HashPin(customer, newPin);

        Console.WriteLine("\nPIN changed successfully!");
    }
}