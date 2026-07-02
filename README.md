# 🏦 Prime Capital Bank

A console-based banking system built in C# .NET, simulating core banking operations such as account creation, authentication, and customer management.

---

## 🚀 Current Version

**v1.0.0**

This version includes a major refactor of the architecture, introducing separation of concerns using service-based design.

---

## 🧠 Features

- Create bank accounts (Checking / Savings)
- Customer registration with ID validation
- Secure PIN creation and authentication
- Account login system
- Display registered customers and accounts
- Account number generation system
- Basic balance tracking

---

## 🏗️ Architecture

The project was refactored from a monolithic structure into a modular service-based architecture:

### 📁 Models
- Customer
- BankAccount
- AccountType (enum)

### ⚙️ Services
- **BankService** → Orchestrates application flow
- **CustomerService** → Handles customer management
- **AccountService** → Handles account operations and formatting
- **AuthenticationService** → Handles secure PIN input

---

## 🔄 Recent Refactor (v1.0.0)

- Split monolithic `BankService` into modular services
- Introduced separation of concerns
- Improved maintainability and scalability
- Reduced code duplication
- Centralized authentication logic

---

## 🎯 Future Improvements

- Deposit and withdrawal functionality
- Money transfer between accounts
- Transaction history tracking
- Data persistence (JSON or database)
- UI improvements (console UX)

---

## 🛠️ Tech Stack

- C# (.NET)
- Console Application
- Object-Oriented Programming (OOP)

---

## 📌 Purpose

This project was built for learning and portfolio purposes, focusing on:

- OOP principles
- Clean architecture
- Service-based design
- Real-world banking logic simulation

---

## 👨‍💻 Author

Developed by Pedro Henrique dos Santos Dantas.
