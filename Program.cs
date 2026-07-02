// Prime Capital Bank Project

using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text.RegularExpressions;

List<Customer> customers = new();
int nextAccountNumber = 1001;

Console.WriteLine("==================================");
Console.WriteLine(@"     🏦 𝐏𝐫𝐢𝐦𝐞 𝐂𝐚𝐩𝐢𝐭𝐚𝐥 𝐁𝐚𝐧𝐤 🏦     ");
Console.WriteLine("==================================");

Console.WriteLine("\nWelcome to Prime Capital Bank! How we can help you?");

while (true)
{
    Console.WriteLine("\n============== MENU ==============");
    Console.WriteLine("\n1 - Open an account.");
    Console.WriteLine("2 - Sign In.");
    Console.WriteLine("3 - Show Registered accounts.");
    Console.WriteLine("4 - Exit\n");
    
    Console.Write("\nChoose an option: ");
    
    if (!int.TryParse(Console.ReadLine(), out int selectedOption))
    {
        Console.WriteLine("\nInvalid option.");
        continue;
    }

    switch (selectedOption)
    {
        case 1:
            CreateAccount();
            break;
        
        case 2:
            SignIn();
            break;

        case 3:
            ShowRegisteredAccounts();
            break;

        default:
            Console.WriteLine("\nInvalid option.");
            break;
    }
}

Customer? FindCustomerById(string idNumber)
{
    return customers.Find(customer => customer.IdNumber == idNumber);
}

void CreateAccount()
{
    Console.Clear();
    Console.WriteLine("\n========== OPEN AN ACCOUNT ==========\n");

    Console.Write("\nPlease enter your full name: ");
    string name = Console.ReadLine()!;

    Console.Write("\nWhat's your date of birth (dd/MM/yyyy): ");
    DateTime birthDate = DateTime.Parse(Console.ReadLine()!);

    string idNumber;

    while (true)
    {
        Console.Write("\nID Number (Only numbers): ");
        string input = Console.ReadLine()!;

        //Remover qualquer caractere que não seja número
        input = new string(input.Where(char.IsDigit).ToArray());

        if (input.Length != 11)
        {
            Console.WriteLine("\nThe ID number must contain exactly 11 digits.\n");
            continue;
        }

        //Formatar automaticamente para: XXX.XXX.XXX-YY
        idNumber = Convert.ToUInt64(input).ToString(@"000\.000\.000\-00");
        break;
    }

    Console.Write("\nApproximate monthly income: ");
    decimal monthlyIncome = decimal.Parse(Console.ReadLine()!);

    Customer? customer = FindCustomerById(idNumber);

// Se o cliente ainda não existir, cria um novo
    if (customer == null)
    {
        Console.WriteLine("\nCreate a 6-digit PIN:");
        string password = ReadPin();

        Console.WriteLine("\nConfirm your PIN: ");
        string confirmPassword = ReadPin();

        while (password != confirmPassword)
        {
            Console.WriteLine("\nThe PINs do not match.\n");

            Console.WriteLine("Create your 6-digit PIN: ");
            password = ReadPin();

            Console.WriteLine("Confirm your PIN: ");
            confirmPassword = ReadPin();
        }

        customer = new Customer
        {
            Name = name,
            BirthDate = birthDate,
            IdNumber = idNumber,
            MonthlyIncome = monthlyIncome,
            Pin = password
        };

        customers.Add(customer);
    }

    Console.WriteLine("\nChoose the account type:\n");
    Console.WriteLine("1 - Checking");
    Console.WriteLine("2 - Savings");

    Console.Write("\nOption: ");

    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("Invalid option.");
        Thread.Sleep(2000);
        return;
    }
    
    AccountType accountType;

    switch (option)
    {
        case 1:
            accountType = AccountType.Checking;
            break;

        case 2:
            accountType = AccountType.Savings;
            break;

        default:
            Console.WriteLine("Invalid option.");
            Thread.Sleep(2000);
            return;
    }

    bool accountAlreadyExists =
        customer.Accounts.Any(account => account.AccountType == accountType);

    if (accountAlreadyExists)
    {
        Console.WriteLine($"\nYou already have a {GetAccountType(accountType)} account.");
        Thread.Sleep(3000);
        return;
    }

    BankAccount account = new BankAccount
    {
        AccountType = accountType,
        AccountNumber = GenerateAccountNumber(accountType),
        Balance = 0,
        CreatedAt = DateTime.Now
    };

customer.Accounts.Add(account);

    Console.Clear();

    Console.WriteLine("======================================");
    Console.WriteLine("Account created successfully!");
    Console.WriteLine("======================================\n");
    Console.WriteLine($"Customer Name : {customer.Name}");
    Console.WriteLine($"Account Number: {account.AccountNumber}");
    Console.WriteLine($"Account Type  : {GetAccountType(account.AccountType)}");
    Console.WriteLine($"Balance       : ${account.Balance:N2}");

    Thread.Sleep(7000);

}

string ReadPin()
{
    string pin = "";

    while (pin.Length < 6)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);

        // Aceita apenas números e no máximo 6 dígitos
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

string GenerateAccountNumber(AccountType accountType)
{
    string suffix = accountType == AccountType.Checking ? "C" : "P";
    string accountNumber = $"{nextAccountNumber:D6}-{suffix}";
    nextAccountNumber++;

    return accountNumber;
}

string GetAccountType(AccountType accountType)
{
    return accountType == AccountType.Checking
    ? "Checking Account"
    : "Savings Account";
}

void ShowRegisteredAccounts()
    {
        Console.Clear();

        if (customers.Count == 0)
        {
           Console.WriteLine("No registered customers found.");
           Thread.Sleep(2000);
           Return();
           return;
        }

         Console.WriteLine("==================== REGISTERED CUSTOMERS ====================\n");

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"Customer Name: {customer.Name}");
                Console.WriteLine($"ID Number: {customer.IdNumber}");
                Console.WriteLine($"Birth Date: {customer.BirthDate:dd/MM/yyyy}");
                Console.WriteLine($"Monthly Income: {customer.MonthlyIncome:N2}");

                foreach (BankAccount account in customer.Accounts)
            {
                Console.WriteLine("\n==================================================\n");
                Console.WriteLine("\nAccounts:\n ");
                Console.WriteLine($"Account Type: {GetAccountType(account.AccountType)}");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Balance: R${account.Balance:N2}");
                Console.WriteLine($"Created At: {account.CreatedAt:dd/MM/yyyy}");
            }

            Console.WriteLine("\n==================================================\n");
            }
             Console.WriteLine("Press any key to return...");
             Console.ReadKey();

             Return();
    }

//Cliente já cadastrado, realizando login na conta.
void SignIn()
{
    Console.Clear();

    Console.WriteLine("========== SIGN IN ==========\n");

    Console.Write("Account Number: ");
    string accountNumber = Console.ReadLine()!;

    Console.Write("Enter your PIN: ");

    string pin = ReadPin()!;

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

    Customer? loggedCustomer = null;
    BankAccount? loggedAccount = null;

    foreach (Customer customer in customers)
    {
        if (customer.Pin != pin)
            continue;

        foreach (BankAccount account in customer.Accounts)
        {
            if (account.AccountNumber == accountNumber)
            {
                loggedCustomer = customer;
                loggedAccount = account;
                break;
            }
        }

        if (loggedCustomer != null)
            break;
    }

    if (loggedCustomer == null || loggedAccount == null)
    {
        Console.WriteLine("\nInvalid account number or PIN.");
        Thread.Sleep(2500);
        Return();
        return;
    }

    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine($"\nWelcome back, {loggedCustomer.Name}\n");
    Console.WriteLine("==========================================\n");
    Console.WriteLine();

    Console.WriteLine($"Member since : {loggedAccount.CreatedAt.Year}");
    Console.WriteLine($"Account Type : {GetAccountType(loggedAccount.AccountType)}");
    Console.WriteLine($"Account No.  : {loggedAccount.AccountNumber}");
    Console.WriteLine($"Balance      : ${loggedAccount.Balance:N2}");

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();

    Return();
}

void Return()
{
    Console.Clear();
}

enum AccountType
{
    Checking,
    Savings
}

class Customer
{
    public string Name { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string IdNumber { get; set; } = "";
    public decimal MonthlyIncome { get; set; }

    public string Pin { get; set; } = "";
    public List<BankAccount> Accounts {get; set; } = new();
}

class BankAccount
{
    public string AccountNumber { get; set; } = "";
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
}

   






// gpg fix test
