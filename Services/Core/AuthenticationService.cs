using System;

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
}