🏦 Prime Capital Bank

A console-based banking system built in C# .NET, simulating core banking operations such as account creation, authentication, customer management, deposits, withdrawals, transfers, investments, income tax calculation, and account statements.

🚀 Current Version

v1.3.0

This version introduces a complete investment module, including portfolio management, investment redemption, compound interest simulation, and an income tax calculator, bringing the application even closer to a real-world banking system.

🧠 Features

- Create bank accounts (Checking / Savings)
- Customer registration with ID validation
- Secure PIN creation and authentication
- Account login system
- Display registered customers and accounts
- Automatic account number generation
- Prevent duplicate account types for the same customer
- Deposit funds
- Withdraw funds
- Transfer money between accounts
- Account statement
- Transaction history with date, amount, and description
- Transfer records displaying the recipient/sender name and account number
- Basic balance tracking
- Fixed income investments
- Investment portfolio management
- Investment redemption
- Compound interest investment simulator
- Income tax calculator for investments

🏗️ Architecture

The project follows a modular service-based architecture.

📁 Models

- Customer
- BankAccount
- Transaction
- Investment
- AccountType (enum)
- InvestmentType (enum)

⚙️ Services

- BankService → Controls the application flow and main menu
- CustomerService → Handles customer management
- AccountService → Handles account creation and account-related operations
- AuthenticationService → Handles secure PIN input
- InvestmentService → Handles investments, portfolio management, redemption, and simulations
- IncomeTaxService → Calculates investment income tax based on Brazilian fixed-income tax rules

🔄 What’s New in v1.3.0

- Added complete investment module
- Implemented fixed income investments
- Added investment portfolio
- Implemented investment redemption
- Added compound interest investment simulator
- Added investment income tax calculator
- Improved project architecture with dedicated investment services
- Expanded banking features with wealth management functionality

🗺️ Roadmap

✅ Completed

- Customer registration
- Account creation
- Secure authentication
- Checking and Savings accounts
- Deposits
- Withdrawals
- Transfers
- Transaction history
- Account statements
- Investment management
- Investment portfolio
- Investment redemption
- Investment simulator
- Income tax calculator

🚧 In Progress

- Improved transaction history
- Dashboard with financial summary

📌 Planned

- Data persistence (JSON)
- SQL Server integration
- Entity Framework Core
- Investment performance comparison
- Monthly investment contributions
- Financial goals
- Unit tests
- Console UI improvements
- REST API
- ASP.NET Core Web API
- Mobile application (Flutter)
- Cloud deployment

🛠️ Tech Stack

- C# (.NET 8)
- Console Application
- Object-Oriented Programming (OOP)
- Service-Based Architecture

📌 Purpose

This project was built for learning and portfolio purposes, focusing on:

* OOP principles
* Clean architecture
* Service-based architecture
* SOLID-oriented design
* Real-world banking logic simulation
* Financial systems development

👨‍💻 Author

Developed by Pedro Henrique dos Santos Dantas.
