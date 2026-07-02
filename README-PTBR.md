<<<<<<< HEAD
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

=======
## Prime Capital Bank

## Versão: 
v0.1.0

Sistema bancário desenvolvido em C# utilizando .NET, criado com o objetivo de práticar a lógica de programação, estruturas de dados e conceitos fundamentais de desenvolvimento de software.

Este projeto faz parte do meu portfólio e será expandido gradualmente com novas funcionalidades, simulando recursos encontrados em sistemas bancários reais.

## Funcionalidades atuais
- Criação de contas bancárias.
- Login.
- Depósito.
- Saque.
- Transfêrencia entre contas.
- Menu interativo no console.
- Validação de entradas do usuário.

## Próximas funcionalidades
- Sistema de autenticação.
- Login utilizando número da conta e senha.
- Bloqueio de operações sem autenticação.
- Alteração de senha.
- Validação de tentativas de acesso.
- Área de investimentos (Simulação de aplicações financeiras, cálculo de rendimento, consulta do patrimônio investido e resgate de investimentos).
- Cálculo de Imposto de Renda (Informar rendimentos mensais, cálculo da média salarial anual, estimativa de Imposto de Renda com base na tabela vigente e relatório de rendimentos e impostos).

## Tecnologias
- C#
- .NET
- SQL Server
- Console Application
- GitHub

## Autor
>>>>>>> 0f175f2 (docs: update README and add pt-BR version)
Desenvolvido por Pedro Henrique dos Santos Dantas.
