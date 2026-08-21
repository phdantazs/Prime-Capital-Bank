🏦 Prime Capital Bank

A banking system developed in C# .NET Console, simulating banking and financial operations such as account creation, authentication, customer management, deposits, withdrawals, transfers, investments, income tax calculation, bank statements, and Bitcoin operations.

The project aims to practically simulate business rules and workflows found in real-world financial systems, with a focus on code organization, validations, edge cases, and incremental application development.

🚀 Current Version

v1.4.0

This version expands the system with the introduction of the Bitcoin module, while maintaining the banking and investment modules and extending the project to support different types of financial operations.

Improvements were also made to business rules, validations, authentication, and service organization.

🧠 Features

🏦 Banking Operations

* Bank account creation (Checking / Savings)
* Customer registration with CPF validation
* Automatic account number generation
* Prevention of duplicate accounts for the same customer
* Account limit per customer
* PIN authentication
* Account login
* PIN change
* Temporary account lock after failed authentication attempts
* Deposits
* Withdrawals
* Transfers between accounts
* Balance management
* Bank statements
* Transaction history
* Transaction date, amount, and description
* Sender/recipient identification for transfers

📈 Investments

* Fixed-income investments
* Treasury Selic
* CDB
* LCI
* LCA
* Fixed Income Fund
* Investment portfolio management
* Investment overview
* Investment redemption
* Investment value calculation
* Return calculation
* Investment simulator
* Compound interest
* Periodic contributions in simulations
* Investment comparison
* Recommended investor profile
* Risk, liquidity, taxation, and FGC protection information
* Income Tax calculation
* Brazilian progressive tax rules for fixed-income investments
* Income Tax exemption for LCI and LCA
* Tax deduction upon redemption

₿ Bitcoin

* Bitcoin account opening
* Bitcoin wallet
* Bitcoin purchases
* Bitcoin sales
* Bitcoin balance
* Bitcoin price lookup
* Bitcoin transaction history
* Bitcoin account closing

The Bitcoin module is still under development, and some features are being further improved.

🏗️ Architecture

The project follows a modular service-based architecture, aiming to separate responsibilities and keep business rules organized.

📁 Models

* Customer
* BankAccount
* Transaction
* Investment
* BitcoinWallet
* BitcoinTransaction
* AccountType
* InvestmentType
* ContributionFrequency
* InvestorProfile
* BitcoinTransactionType

⚙️ Services

* BankService → Controls the application flow and main menus
* CustomerService → Manages customers
* AccountService → Responsible for account creation and management
* AuthenticationService → Handles authentication, validation, and PIN management
* InvestmentService → Manages investments, portfolios, redemptions, and simulations
* TaxService → Calculates Income Tax on investments
* BitcoinService → Manages Bitcoin wallets, purchases, sales, and transactions
* InputService → Centralizes user input and data validation

🔄 What’s New in v1.4.0

* Added Bitcoin module
* Implemented Bitcoin account opening
* Implemented Bitcoin wallet
* Implemented Bitcoin purchase and sale operations
* Added Bitcoin transaction history
* Added Bitcoin balance lookup
* Integrated the Bitcoin module into the main banking flow
* Improved input validation
* Improved business rules and edge case handling
* Expanded the system to support banking, investment, and crypto asset operations

🗺️ Roadmap

✅ Completed

* Customer registration
* Bank account creation
* PIN authentication
* Temporary lock after failed authentication attempts
* Checking and Savings accounts
* Deposits
* Withdrawals
* Transfers
* Transaction history
* Bank statements
* Investment management
* Investment portfolio
* Fixed-income investments
* Investment redemption
* Investment simulator
* Compound interest
* Income Tax calculator
* Bitcoin module
* Bitcoin wallet
* Bitcoin purchase and sale
* Bitcoin transaction history

🚧 In Development

* Bitcoin module improvements
* InputService improvements and standardization
* Error handling and application robustness
* Refactoring and overall system review
* Console interface improvements

📌 Next Steps

* Data persistence
* SQL Server integration
* Entity Framework Core
* Unit testing
* Messaging and notifications
* APIs
* ASP.NET Core Web API
* External service integrations
* Cloud storage
* Cloud deployment
* Mobile application with Flutter

🛠️ Technologies

* C#
* .NET 8
* Console Application
* Object-Oriented Programming (OOP)
* Service-based Architecture
* Git
* GitHub

Future Technologies

* SQL Server
* Entity Framework Core
* ASP.NET Core Web API
* Messaging services
* Cloud

📌 Objective

This project was developed for learning and portfolio purposes, focusing on the progressive development of a financial system and the practical application of software development concepts.

Main objectives:

* Practice Object-Oriented Programming
* Apply SOLID principles
* Develop and organize business rules
* Work with separation of responsibilities
* Simulate real-world financial operations
* Handle validations and edge cases
* Develop financial systems using C#
* Incrementally evolve application architecture
* Use Git and GitHub throughout the development process

👨‍💻 Author

Developed by Pedro Henrique dos Santos Dantas.
