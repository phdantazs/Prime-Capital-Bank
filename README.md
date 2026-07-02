## 🏦 Prime Capital Bank

A console-based banking system built in C# .NET, simulating core banking operations such as account creation, authentication, and customer management.

## 🚀 Current Version

v1.1.0

This version focuses on improving the project’s architecture by organizing the application into dedicated services, making the code cleaner, more maintainable, and easier to extend.

## 🧠 Features

* Create bank accounts (Checking / Savings)
* Customer registration with ID validation
* Secure PIN creation and authentication
* Account login system
* Display registered customers and accounts
* Automatic account number generation
* Prevent duplicate account types for the same customer
* Basic balance tracking

## 🏗️ Architecture

The project follows a modular service-based architecture.

## 📁 Models

* Customer
* BankAccount
* AccountType (enum)

## ⚙️ Services

* BankService → Controls the application flow and main menu
* CustomerService → Handles customer management
* AccountService → Handles account creation and account-related operations
* AuthenticationService → Handles secure PIN input

## 🔄 What’s New in v1.1.0

* Reorganized the application into dedicated services
* Simplified the Program.cs entry point
* Improved separation of responsibilities
* Reduced duplicated code
* Centralized account creation logic
* Improved overall project organization for future features

## 🎯 Future Improvements

* Deposit and withdrawal functionality
* Money transfers between accounts
* Transaction history
* Data persistence (JSON or database)
* Console UI improvements

## 🛠️ Tech Stack

* C# (.NET)
* Console Application
* Object-Oriented Programming (OOP)

## 📌 Purpose

This project was built for learning and portfolio purposes, focusing on:

* OOP principles
* Clean architecture
* Service-based design
* Real-world banking logic simulation

## 👨‍💻 Author

Developed by Pedro Henrique dos Santos Dantas.
