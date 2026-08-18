using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Linq;

namespace PrimeCapitalBank.Services;

public class InputService
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

    public string ReadIdNumber()
    {
        string idNumber = "";

        while (idNumber.Length < 11)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (idNumber.Length == 0)
                    continue;
                //Apaga o último digito armazenado
                idNumber = idNumber[..^1];
                //Apaga o último caractere exibido
                Console.Write("\b \b");
                //Se o caractere anterior era um separador, também apaga
                if (idNumber.Length == 3 || idNumber.Length == 6)
                    Console.Write("\b \b");
                if (idNumber.Length == 9)
                    Console.Write("\b \b");

                continue;
            }

            if (!char.IsDigit(key.KeyChar))
                continue;
            if (idNumber.Length == 3 || idNumber.Length == 6)
                Console.Write(".");
            if (idNumber.Length == 9)
                Console.Write("-");
            
            idNumber += key.KeyChar;
            Console.Write(key.KeyChar);
        }

        Console.WriteLine();
        return FormatIdNumber(idNumber);
    }

    private string FormatIdNumber(string idNumber)
    {
        if (idNumber.Length != 11)
        return idNumber;

        return $"{idNumber[..3]}.{idNumber.Substring(3, 3)}.{idNumber.Substring(6, 3)}-{idNumber.Substring(9, 2)}";
    }

    public DateTime ReadBirthDate()
    {
        while (true)
        {
            string birthDate = "";
        
            while (birthDate.Length < 8)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (birthDate.Length == 0)
                    continue;
                
                birthDate = birthDate[..^1];
                
                Console.Write("\b \b");

                if (birthDate.Length == 2 || birthDate.Length == 4)
                    {
                         Console.Write("\b \b");
                         continue;
                    }
            }

            if (!char.IsDigit(key.KeyChar))
                continue;
            if (birthDate.Length == 2 || birthDate.Length == 4)
                Console.Write("/");

            birthDate += key.KeyChar;
            Console.Write(key.KeyChar);
        }
        Console.WriteLine();

            if (DateTime.TryParseExact(
                birthDate,
                "ddMMyyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime result))
        {
            return result;
        }

        Console.WriteLine("\nInvalid date. Please try again.\n");
        Console.Write("What's your date of birth: ");   
        }
    }

    private const int MoneyFieldWidth = 20;
    public decimal ReadMoney(string prompt)
    {
        string digits = "";

        Console.Write(prompt);

        int left = Console.CursorLeft;
        int top = Console.CursorTop;

        DrawMoney(left, top, digits);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Enter:

                    if (digits.Length == 0)
                    continue;

                    Console.WriteLine();
                    return ParseMoney(digits);

                case ConsoleKey.Backspace:

                    if (digits.Length > 0)
                    {
                        digits = digits[..^1];
                        DrawMoney(left, top, digits);
                    }

                    continue;
            }

            {
            if (!char.IsDigit(key.KeyChar))
                continue;

            digits += key.KeyChar;
            RedrawMoney(left, top, digits);
            }
        }
    }

    public void RedrawMoney(int left, int top, string digits)
    {
        decimal value = 0;

        if (digits.Length > 0)
            value = decimal.Parse(digits) / 100;

        Console.SetCursorPosition(left, top);
        //Limpa toda área do valor
        Console.Write(new string(' ', 20));
        //Volta para o início do valor
        Console.SetCursorPosition(left, top);
        //Escreve novamente
        Console.Write($"R$ {value:N2}");
    }

    private decimal ParseMoney(string digits)
    {
        if (string.IsNullOrEmpty(digits))
            return 0m;

        return decimal.TryParse(digits, out decimal value)
            ? value / 100
            : 0m;
    }

    private void DrawMoney(int left, int top, string digits)
    {
        decimal value = ParseMoney(digits);
        Console.SetCursorPosition(left, top);
        Console.Write(new string(' ', MoneyFieldWidth));
        Console.SetCursorPosition(left, top);
        Console.Write($"R$ {value:N2}");
    }

    public string ReadFullName()
    {
        while (true)
        {
            string? fullName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                Console.Write("Please enter your full name: ");
                continue;
            }
            if (!IsValidFullName(fullName))
            {
                Console.Write("Invalid name. Please enter only letters: ");
                continue;
            }
            return FormatFullName(fullName);
        }
    }

    private bool IsValidFullName(string fullName)
    {
        foreach (char character in fullName)
        {
            if (!char.IsLetter(character) &&
            character != ' ' &&
            character != '-' &&
            character != '\'')
            {
                return false;
            }
        }

        return true;
    }

    private string FormatFullName(string fullName)
    {
        string[] lowercaseWords =
        {
            "da",
            "de",
            "del",
            "di",
            "do",
            "dos",
            "das",
            "e",
            "von",
            "van",
            "O'",
            "bin",
            "al",
            "el",
            "binti",
            "abu"
        };

        string[] lowercasePairs =
        {
            "van der",
            "van den"
        };

        string[] words = fullName
            .Trim()
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < words.Length; i++)
        {
            words[i] = FormatWord(words[i]);
        }

        for (int i = 0; i < words.Length; i++)
        {
            if (lowercaseWords.Contains(words[i].ToLower()))
            {
                words[i] = words[i].ToLower();
            }
            if (i > 0)
            {
            string pair = $"{words[i - 1].ToLower()} {words[i].ToLower()}";

            if (lowercasePairs.Contains(pair))
                {
                    words[i - 1] = words[i - 1].ToLower();
                    words[i] = words[i].ToLower();
                }
            }
        }

        return string.Join(" ", words);
    }

private string FormatWord(string word)
{
    if (string.IsNullOrWhiteSpace(word))
        {
            return word;
        }
            //Trata nomes com hifen (Jean-Pierre)
            if (word.Contains("-"))
            {
                string[] parts = word.Split('-');

                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = FormatWord(parts[i]);
                }

                return string.Join("-", parts);
            }

            //Trata nomes com apóstrofo como O'Connor, D'Angelo
            if (word.Contains("'"))
            {
                string[] parts = word.Split('\'');
                if (parts.Length == 2)
                {
                    return $"{Capitalize(parts[0])}'{Capitalize(parts[1])}";
                }
            }

            //Trata sobrenomes como McGregor, McGinn, McDonald
            if (word.StartsWith("mc", StringComparison.OrdinalIgnoreCase) && word.Length > 2)
            {
                return "Mc" + char.ToUpper(word[2]) + word[3..].ToLower();
            }

            //Trata sobrenomes como MacAllister, MacEntire, MacDonald
            if (word.StartsWith("mac", StringComparison.OrdinalIgnoreCase) && word.Length > 3)
            {
                return "Mac" + char.ToUpper(word[3]) + word[4..].ToLower();
            }

            //Trata nomes como DuPont, DuPlessis, DuBois
            if (word.StartsWith("du", StringComparison.OrdinalIgnoreCase) && word.Length > 2)
            {
                return "Du" + char.ToUpper(word[2]) + word[3..].ToLower();
            }

            return Capitalize(word);
}
        private string Capitalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return word;
        }

        return char.ToUpper(word[0]) + word[1..].ToLower();
    }

    public int ReadMenuOption(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine()?.Trim() ?? "";

            if (int.TryParse(input, out int option) &&
                option >= min &&
                option <= max)
            {
                return option;
            }

            Console.Write($"\nInvalid option. Choose a number between {min} and {max}: ");
        }
    }

    public decimal ReadBitcoinAmount(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (decimal.TryParse(
                input,
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal value))
            {
                return value;
            }

            Console.WriteLine("\nInvalid Bitcoin amount. Please try again.");
        }
    }
}