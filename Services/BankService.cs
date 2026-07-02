using PrimeCapitalBank.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PrimeCapitalBank.Services;

public class BankService
{
    private readonly List<Customer> customers = new();
    private readonly CustomerService _customerService;
    private readonly AccountService _accountService;
    private readonly AuthenticationService _authenticationService;
    public BankService()
    {
        _customerService = new CustomerService(customers);
        _accountService = new AccountService();
        _authenticationService = new AuthenticationService();
    }
public void Start()
{
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

        case 4:
            Console.WriteLine("\nFinalizing session...");
            Thread.Sleep(1500);
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("\nInvalid option.");
            break;
    }
}

}

//********** CREATE ACCOUNT **********
public void CreateAccount()
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

    Customer? customer = _customerService.FindCustomerById(idNumber);

// Se o cliente ainda não existir, cria um novo
    if (customer == null)
    {
        Console.WriteLine("\nCreate a 6-digit PIN:");
        string pin = _authenticationService.ReadPin();

        Console.WriteLine("\nConfirm your PIN: ");
        string confirmPin = _authenticationService.ReadPin();

        while (pin != confirmPin)
        {
            Console.WriteLine("\nPINs do not match. Try again.\n");

            pin = _authenticationService.ReadPin();
            confirmPin = _authenticationService.ReadPin();
        }

        customer = new Customer
        {
            Name = name,
            BirthDate = birthDate,
            IdNumber = idNumber,
            MonthlyIncome = monthlyIncome,
            Pin = pin
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
    
    if (option == 1)
    {
        accountType = AccountType.Checking;
    }
    else if (option == 2)
    {
        accountType = AccountType.Savings;
    }
        else
        {
            Console.WriteLine("\nInvalid option.");
            Thread.Sleep(2000);
            return;
        }

    bool accountAlreadyExists = customer.Accounts.Any(a => a.AccountType == accountType);

    if (accountAlreadyExists)
    {
        Console.WriteLine($"\nYou already have a {_accountService.GetAccountType(accountType)} account.");
        Thread.Sleep(2000);
        return;
    }

    BankAccount account = _accountService.CreateAccount(accountType);

    customer.Accounts.Add(account);

    Console.Clear();

    Console.WriteLine("======================================");
    Console.WriteLine("Account created successfully!");
    Console.WriteLine("======================================\n");
    Console.WriteLine($"Customer Name : {customer.Name}");
    Console.WriteLine($"Account Number: {account.AccountNumber}");
    Console.WriteLine($"Account Type  : {_accountService.GetAccountType(account.AccountType)}");
    Console.WriteLine($"Balance       : R${account.Balance:N2}");

    Thread.Sleep(5000);
}

//Show Accounts
public void ShowRegisteredAccounts()
    {
        Console.Clear();

        if (customers.Count == 0)
        {
           Console.WriteLine("No registered customers found.");
           Thread.Sleep(2000);
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
                Console.WriteLine($"Account Type: {_accountService.GetAccountType(account.AccountType)}");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Balance: R${account.Balance:N2}");
                Console.WriteLine($"Created At: {account.CreatedAt:dd/MM/yyyy}");
            }

            Console.WriteLine("\n==================================================\n");
            }
             Console.WriteLine("Press any key to return...");
             Console.ReadKey();
             Console.Clear();
    }

//Sign In
public void SignIn()
{
    Console.Clear();

    Console.WriteLine("========== SIGN IN ==========\n");

    Console.Write("Account Number: ");
    string accountNumber = Console.ReadLine()!;

    Console.Write("Enter your PIN: ");
    string pin = _authenticationService.ReadPin()!;

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
        Console.WriteLine("\nInvalid credentials.");
        Thread.Sleep(2000);
        return;
    }

    Console.Clear();

    Console.WriteLine("==========================================");
    Console.WriteLine($"\nWelcome back, {loggedCustomer.Name}!\n");
    Console.WriteLine("==========================================\n");
    Console.WriteLine();

    Console.WriteLine($"Member since : {loggedAccount.CreatedAt.Year}");
    Console.WriteLine($"Account Type : {_accountService.GetAccountType(loggedAccount.AccountType)}");
    Console.WriteLine($"Account No.  : {loggedAccount.AccountNumber}");
    Console.WriteLine($"Balance      : ${loggedAccount.Balance:N2}");

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

}
