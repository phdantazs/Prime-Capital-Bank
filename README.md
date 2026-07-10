🏦 Prime Capital Bank

A console-based banking system built in C# .NET, simulating core banking operations such as account creation, authentication, customer management, deposits, withdrawals, transfers, and account statements.

🚀 Current Version

v1.2.0

This version introduces core banking transactions, transaction history, and account statements, making the system more complete and closer to a real-world banking application.

🧠 Features

* Create bank accounts (Checking / Savings)
* Customer registration with ID validation
* Secure PIN creation and authentication
* Account login system
* Display registered customers and accounts
* Automatic account number generation
* Prevent duplicate account types for the same customer
* Deposit funds
* Withdraw funds
* Transfer money between accounts
* Account statement
* Transaction history with date, amount, and description
* Transfer records displaying the recipient/sender name and account number
* Basic balance tracking

🏗️ Architecture

The project follows a modular service-based architecture.

📁 Models

* Customer
* BankAccount
* Transaction
* AccountType (enum)

⚙️ Services

* BankService → Controls the application flow and main menu
* CustomerService → Handles customer management
* AccountService → Handles account creation and account-related operations
* AuthenticationService → Handles secure PIN input

🔄 What’s New in v1.2.0

* Implemented deposit functionality
* Implemented withdrawal functionality
* Implemented money transfers between accounts
* Added transaction history
* Added account statement
* Improved transfer descriptions with customer name and account number
* Improved banking workflow and business rules

🎯 Future Improvements

* Data persistence (JSON or database)
* Transaction filtering
* Unit tests
* Console UI improvements
* Investment simulator
* Income tax calculator

🛠️ Tech Stack

* C# (.NET)
* Console Application
* Object-Oriented Programming (OOP)

📌 Purpose

This project was built for learning and portfolio purposes, focusing on:

* OOP principles
* Clean architecture
* Service-based design
* Real-world banking logic simulation

👨‍💻 Author

Developed by Pedro Henrique dos Santos Dantas.
