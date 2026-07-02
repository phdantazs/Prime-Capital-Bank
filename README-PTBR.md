# 🏦 Prime Capital Bank

Sistema bancário em console desenvolvido em C# .NET, simulando operações bancárias essenciais como criação de contas, autenticação e gerenciamento de clientes.

---

## 🚀 Versão Atual

**v1.0.0**

Esta versão inclui uma grande refatoração da arquitetura, introduzindo separação de responsabilidades utilizando um design baseado em serviços.

---

## 🧠 Funcionalidades

- Criar contas bancárias (Corrente / Poupança)
- Cadastro de clientes com validação de CPF (ID)
- Criação e autenticação de PIN seguro
- Sistema de login de contas
- Exibição de clientes e contas cadastradas
- Geração automática de número de conta
- Controle básico de saldo

---

## 🏗️ Arquitetura

O projeto foi refatorado de uma estrutura monolítica para uma arquitetura modular baseada em serviços:

### 📁 Models
- Customer (Cliente)
- BankAccount (Conta bancária)
- AccountType (Enum de tipo de conta)

### ⚙️ Services
- **BankService** → Orquestra o fluxo principal da aplicação
- **CustomerService** → Gerencia clientes
- **AccountService** → Gerencia contas e formatação
- **AuthenticationService** → Responsável pela entrada segura de PIN

---

## 🔄 Refatoração Recente (v1.0.0)

- Separação do `BankService` monolítico em serviços menores
- Aplicação do princípio de responsabilidade única (SRP)
- Melhoria na manutenção e escalabilidade do sistema
- Redução de duplicação de código
- Centralização da lógica de autenticação

---

## 🎯 Melhorias Futuras

- Funcionalidade de depósito e saque
- Transferência entre contas
- Histórico de transações
- Persistência de dados (JSON ou banco de dados)
- Melhorias na experiência de uso do console

---

## 🛠️ Tecnologias Utilizadas

- C# (.NET)
- Aplicação Console
- Programação Orientada a Objetos (POO)

---

## 📌 Objetivo do Projeto

Este projeto foi desenvolvido com foco em aprendizado e portfólio, aplicando:

- Princípios de POO
- Arquitetura limpa
- Design baseado em serviços
- Simulação de lógica bancária real

---

## 👨‍💻 Autor

Desenvolvido por Pedro Henrique dos Santos Dantas.
