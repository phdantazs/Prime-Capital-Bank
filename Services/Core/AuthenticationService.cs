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

            if (currentPin is not null && pin == currentPin)
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

    public bool Authenticate(Customer customer, string pin)
    {
        // Verifica se a conta está bloqueada
        if (customer.BlockedUntil.HasValue)
    {
        if (DateTime.UtcNow < customer.BlockedUntil.Value)
        {
            TimeSpan remaining = customer.BlockedUntil.Value - DateTime.UtcNow;

            Console.WriteLine($"\nThis account is temporarily blocked. Try again in {remaining.Minutes:D2}:{remaining.Seconds:D2}.");

            return false;
        }

        // Unblock the account automatically
        customer.BlockedUntil = null;
        customer.FailedLoginAttempts = 0;
    }

    PasswordVerificationResult result = 
        _passwordHasher.VerifyHashedPassword(
                customer,
                customer.PinHash,
                pin);

    // Validate the PIN

    if (result == PasswordVerificationResult.Failed)
    {
        customer.FailedLoginAttempts++;
        
        if (customer.FailedLoginAttempts >= 3)
            {
                customer.BlockedUntil = DateTime.UtcNow.AddMinutes(2);
                Console.WriteLine("\nYour account has been temporarily blocked for 2 minutes.");
            }

            else
            {
                int remainingAttempts = 3 - customer.FailedLoginAttempts;

                Console.WriteLine($"\nInvalid PIN, {remainingAttempts} attempt(s) remaining");
            }

        return false;
    }

    customer.FailedLoginAttempts = 0;
    customer.BlockedUntil = null;

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