// Prime Capital Bank Project

using PrimeCapitalBank.Services;

Console.WriteLine("==================================");
Console.WriteLine(@"     🏦 𝐏𝐫𝐢𝐦𝐞 𝐂𝐚𝐩𝐢𝐭𝐚𝐥 𝐁𝐚𝐧𝐤 🏦     ");
Console.WriteLine("==================================");

Console.WriteLine("\nWelcome to Prime Capital Bank! How we can help you?");

BankService bank = new BankService();

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
            bank.CreateAccount();
            break;
        
        case 2:
            bank.SignIn();
            break;

        case 3:
            bank.ShowRegisteredAccounts();
            break;

        default:
            Console.WriteLine("\nInvalid option.");
            break;
    }
}