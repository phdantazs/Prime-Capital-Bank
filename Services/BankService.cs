using PrimeCapitalBank.Models;
using PrimeCapitalBank.Models.Enums;
using PrimeCapitalBank.Services.Core;
using PrimeCapitalBank.Services.Investments;
using PrimeCapitalBank.Services.Bitcoin;
using System.Globalization;

namespace PrimeCapitalBank.Services;

public class BankService
{
    private readonly List<Customer> customers = new();
    private readonly CustomerService _customerService;
    private readonly AccountService _accountService;
    private readonly AuthenticationService _authenticationService;
    private readonly InvestmentService _investmentService;
    private readonly InputService _inputService;
    private readonly BitcoinService _bitcoinService;
    public BankService()
    {
        _customerService = new CustomerService(customers);
        _accountService = new AccountService();

        _inputService = new InputService();
        _authenticationService = new AuthenticationService(_inputService);
        _investmentService = new InvestmentService(_inputService, new TaxService());
        _bitcoinService = new BitcoinService(_inputService);
        
    }
public void Start()
{
    Console.WriteLine("\n==================================");
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
    
    int option = _inputService.ReadMenuOption(1, 4);

    switch (option)
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
            Console.WriteLine("\nLeaving menu...");
            Thread.Sleep(1500);
            return;

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
    Console.WriteLine("========== OPEN AN ACCOUNT ==========\n");

    Console.Write("Please enter your full name: ");
    string fullName = _inputService.ReadFullName();
    Console.WriteLine();

    Console.Write("What's your date of birth (dd/MM/yyyy): ");
    DateTime birthDate = _inputService.ReadBirthDate();
    Console.WriteLine();

    Console.Write("ID Number: ");
    string idNumber = _inputService.ReadIdNumber();
    Console.WriteLine();

    decimal monthlyIncome = _inputService.ReadMoney("Approximate monthly income: ");
    Console.WriteLine();

    Customer? customer = _customerService.FindCustomerById(idNumber);

    string pin;

    // Se o cliente ainda não existir, cria um novo
    if (customer == null)
    {
        pin = _authenticationService.CreatePin();

        customer = new Customer
        {
            Name = fullName,
            BirthDate = birthDate,
            IdNumber = idNumber,
            MonthlyIncome = monthlyIncome,
        };

        customers.Add(customer);
    }
        else
        {
            pin = customer.Accounts.First().Pin;
        }

    Console.WriteLine("\nChoose the account type:\n");
    Console.WriteLine("1 - Checking");
    Console.WriteLine("2 - Savings");

    Console.Write("\nOption: ");

    int option = _inputService.ReadMenuOption(1, 2);

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
    account.Pin = pin;
    account.Owner = customer;
    
    customer.Accounts.Add(account);

    Console.Clear();

    Console.WriteLine("======================================");
    Console.WriteLine("    Account created successfully!");
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
                Console.WriteLine("----------------------------------------------\n");
                Console.WriteLine("Accounts:");
                Console.WriteLine($"\nAccount Type: {_accountService.GetAccountType(account.AccountType)}");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Balance: R${account.Balance:N2}");
                Console.WriteLine($"Created At: {account.CreatedAt:dd/MM/yyyy}");
                Console.WriteLine("----------------------------------------------\n");
            }

            }
             Console.WriteLine("\nPress any key to return...");
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

    Console.Write("\nEnter your PIN: ");
    string pin = _inputService.ReadPin()!;

    Console.WriteLine();

    Customer? loggedCustomer = null;
    BankAccount? loggedAccount = null;

    foreach (Customer customer in customers)
    {
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
        Console.WriteLine("Invalid credentials.");
        Thread.Sleep(3000);
        return;
    }

    if (!_authenticationService.Authenticate(loggedAccount, pin))
        {
            Thread.Sleep(3000);
            return;
        }
    
    Console.Clear();

    Console.WriteLine("===================================");
    Console.WriteLine($"\nWelcome back, {loggedCustomer.Name}!\n");
    Console.WriteLine("===================================\n");

    Console.WriteLine($"Member since : {loggedAccount.CreatedAt.Year}");
    Console.WriteLine($"Account Type : {_accountService.GetAccountType(loggedAccount.AccountType)}");
    Console.WriteLine($"Account No.  : {loggedAccount.AccountNumber}");
    Console.WriteLine($"Balance      : ${loggedAccount.Balance:N2}\n");

    while (true)
        {
            Console.WriteLine("========== ACCOUNT MENU ==========");
            Console.WriteLine("\n1 - Deposit");
            Console.WriteLine("\n2 - Withdraw");
            Console.WriteLine("\n3 - Transfer");
            Console.WriteLine("\n4 - Statement");
            Console.WriteLine("\n5 - Investments");
            Console.WriteLine("\n6 - Bitcoin");
            Console.WriteLine("\n7 - Change PIN");
            Console.WriteLine("\n8 - Logout\n");

            Console.Write("\nWhat you need?: ");
            int option = _inputService.ReadMenuOption(1, 8);
            
            Console.WriteLine();
            Console.WriteLine("============================\n");

        switch (option)
        {
            case 1:
                decimal amount = _inputService.ReadMoney("Enter the deposit amount: ");
                _accountService.Deposit(loggedAccount, amount);
                break;
            
            case 2:
                decimal withdrawalAmount = _inputService.ReadMoney("Withdrawal amount: ");
                _accountService.Withdraw(loggedAccount, withdrawalAmount);
                break;
        
            case 3:
                Console.Write("Destination account number: ");
                string destinationAccountNumber = Console.ReadLine()!;

                BankAccount? destinationAccount = null;

                foreach (Customer customer in customers)
                    {
                        destinationAccount = customer.Accounts
                            .FirstOrDefault(a => a.AccountNumber == destinationAccountNumber);

                            if (destinationAccount != null)
                            break;
                    }

                    if (destinationAccount == null)
                    {
                        Console.WriteLine("\nDestination account not found.");
                        Thread.Sleep(2000);
                        break;
                    }
                    
                    if (destinationAccount == loggedAccount)
                    {
                        Console.WriteLine("\nYou cannot transfer to your own account.");
                        Thread.Sleep(2000);
                        break;
                    }

                    Console.WriteLine("\nTransfer amount: ");

                    if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
                    {
                        Console.WriteLine("\nInvalid amount.");
                        Thread.Sleep(2000);
                        break;
                    }

                    _accountService.Transfer(loggedAccount, destinationAccount, transferAmount);

                    break;

            case 4:
                _accountService.Statement(loggedAccount);
                break;

            case 5:
                OpenInvestmentMenu(loggedAccount);
                break;

            case 6:
                OpenBitcoinMenu(loggedAccount);
                break;

            case 7:
                _authenticationService.ChangePin(loggedCustomer!);
                Thread.Sleep(3000);
                Console.Clear();
                break;

            case 8: 
                Console.WriteLine("\nLeaving menu...");
                Thread.Sleep(1300);
                Console.Clear();
                return;

            default:
                Console.WriteLine("\nInvalid option.");
                break;

        }
    }
}

private void OpenInvestmentMenu(BankAccount account)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("========== INVESTMENTS ==========\n");
            
            Console.WriteLine("1 - Invest");
            Console.WriteLine("2 - My Portfolio");
            Console.WriteLine("3 - Redeem Investment");
            Console.WriteLine("4 - Investment Simulator");
            Console.WriteLine("5 - Back");

            Console.Write("\nOption: ");
            int option = _inputService.ReadMenuOption(1, 5);

            Console.WriteLine("\n========================\n");

            switch (option)
            {
                case 1:
                    _investmentService.Invest(account);
                    break;
                
                case 2:
                    _investmentService.ShowPortfolio(account);
                    break;

                case 3:
                    _investmentService.Redeem(account);
                    break;

                case 4:
                    _investmentService.SimulateInvestment(account);
                    break;

                case 5:
                    Console.Clear();
                    return;
            }
        }
    }

    private void OpenBitcoinMenu(BankAccount account)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("========== BITCOIN ==========\n");

            if (account.BitcoinWallet == null)
            {
                Console.WriteLine("1 - Open Bitcoin Account");
                Console.WriteLine("2 - Back");

                Console.Write("\nOption: ");

                int option = _inputService.ReadMenuOption(1, 2);

                switch (option)
                {
                    case 1:
                        _bitcoinService.OpenBitcoinAccount(account);
                        Thread.Sleep(3000);
                        break;

                    case 2:
                        Console.WriteLine();
                        Console.Clear();
                        return;
                }
            }
            else
            {
                Console.Clear();

                Console.WriteLine("Your Bitcoin account is active!\n");
                Console.WriteLine($"Bitcoin balance: {account.BitcoinWallet.Balance.ToString("F8", CultureInfo.InvariantCulture)} BTC");

                Console.WriteLine("\n1 - Buy Bitcoin");
                Console.WriteLine("2 - Sell Bitcoin");
                Console.WriteLine("3 - My Wallet");
                Console.WriteLine("4 - Bitcoin Price");
                Console.WriteLine("5 - Bitcoin Transactions");
                Console.WriteLine("6 - Close Bitcoin Account");
                Console.WriteLine("7 - Back");

                Console.Write("\nOption: ");

                int option = _inputService.ReadMenuOption(1, 7);

                switch (option)
                {
                    case 1:
                        _bitcoinService.BuyBitcoin(account);
                        break;

                    case 2:
                        _bitcoinService.SellBitcoin(account);
                        break;

                    case 3:
                        _bitcoinService.ShowBitcoinWallet(account);
                        break;
                    
                    case 4:
                        _bitcoinService.ShowBitcoinPrice();
                        break;

                    case 5:
                        _bitcoinService.ShowBitcoinTransactions(account);
                        break;

                    case 6:
                        _bitcoinService.CloseBitcoinAccount(account);
                        break;

                    case 7:
                        Console.Clear();
                        return;
                }
            }
        }
    }
}