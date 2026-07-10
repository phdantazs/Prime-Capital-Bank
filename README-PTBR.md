🏦 Prime Capital Bank

Um sistema bancário desenvolvido em C# .NET para console, simulando operações bancárias essenciais, como criação de contas, autenticação, gerenciamento de clientes, depósitos, saques, transferências e extrato bancário.

🚀 Versão Atual

v1.2.0

Esta versão adiciona as principais operações bancárias, histórico de transações e extrato da conta, tornando o sistema mais completo e próximo de uma aplicação bancária real.

🧠 Funcionalidades

* Criação de contas bancárias (Corrente / Poupança)
* Cadastro de clientes com validação de documento
* Criação e autenticação segura de PIN
* Sistema de login da conta
* Exibição dos clientes e contas cadastrados
* Geração automática do número da conta
* Impede contas duplicadas do mesmo tipo para o mesmo cliente
* Depósitos
* Saques
* Transferências entre contas
* Extrato da conta
* Histórico de transações com data, valor e descrição
* Transferências exibindo o nome do destinatário/remetente e o número da conta
* Controle básico de saldo

🏗️ Arquitetura

O projeto segue uma arquitetura modular baseada em serviços.

📁 Models

* Customer
* BankAccount
* Transaction
* AccountType (enum)

⚙️ Services

* BankService → Controla o fluxo da aplicação e o menu principal
* CustomerService → Gerencia os clientes
* AccountService → Gerencia a criação de contas e as operações bancárias
* AuthenticationService → Gerencia a entrada segura do PIN

🔄 Novidades da v1.2.0

* Implementação de depósitos
* Implementação de saques
* Implementação de transferências entre contas
* Adição de histórico de transações
* Adição de extrato bancário
* Melhoria na descrição das transferências com nome do cliente e número da conta
* Aprimoramento do fluxo bancário e das regras de negócio

🎯 Próximas Melhorias

* Persistência de dados (JSON ou banco de dados)
* Filtro de transações
* Testes unitários
* Melhorias na interface do console
* Simulador de investimentos
* Calculadora de imposto de renda

🛠️ Tecnologias

* C# (.NET)
* Aplicação Console
* Programação Orientada a Objetos (POO)

📌 Objetivo

Este projeto foi desenvolvido para fins de aprendizado e portfólio, com foco em:

* Princípios de POO
* Arquitetura limpa
* Arquitetura baseada em serviços
* Simulação de regras de negócio de um sistema bancário real

👨‍💻 Autor

Desenvolvido por Pedro Henrique dos Santos Dantas.
