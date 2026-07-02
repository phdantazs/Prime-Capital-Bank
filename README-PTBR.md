## 🏦 Prime Capital Bank

Um sistema bancário em console desenvolvido em C# .NET, simulando operações bancárias como criação de contas, autenticação e gerenciamento de clientes.

## 🚀 Versão Atual

v1.1.0

Esta versão tem como foco a melhoria da arquitetura do projeto, organizando a aplicação em serviços específicos para tornar o código mais limpo, organizado e preparado para futuras funcionalidades.

## 🧠 Funcionalidades

* Criação de Conta Corrente e Conta Poupança
* Cadastro de clientes com validação do documento
* Criação e autenticação por PIN de 6 dígitos
* Sistema de login por conta bancária
* Listagem de clientes e contas cadastradas
* Geração automática do número da conta
* Impede contas duplicadas do mesmo tipo para um mesmo cliente
* Controle básico de saldo

## 🏗️ Arquitetura

O projeto segue uma arquitetura modular baseada em serviços.

## 📁 Models

* Customer
* BankAccount
* AccountType (enum)

## ⚙️ Services

* BankService → Controla o fluxo da aplicação e o menu principal
* CustomerService → Gerencia os clientes
* AccountService → Responsável pela criação e operações relacionadas às contas
* AuthenticationService → Responsável pela leitura segura do PIN

## 🔄 Novidades da v1.1.0

* Reorganização da aplicação em serviços especializados
* Simplificação do Program.cs
* Melhor separação de responsabilidades
* Redução de código duplicado
* Centralização da lógica de criação de contas
* Estrutura preparada para futuras funcionalidades

## 🎯 Próximas Melhorias

* Depósitos e saques
* Transferências entre contas
* Histórico de transações
* Persistência de dados (JSON ou banco de dados)
* Melhorias na interface em console

## 🛠️ Tecnologias

* C# (.NET)
* Aplicação Console
* Programação Orientada a Objetos (POO)

## 📌 Objetivo

Este projeto foi desenvolvido para aprendizado e portfólio, com foco em:

* Princípios de POO
* Arquitetura limpa
* Arquitetura baseada em serviços
* Simulação de regras de negócio de um sistema bancário

## 👨‍💻 Autor

Desenvolvido por Pedro Henrique dos Santos Dantas.
