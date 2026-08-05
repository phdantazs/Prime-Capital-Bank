🏦 Prime Capital Bank

Um sistema bancário desenvolvido em C# .NET Console, simulando operações bancárias essenciais, como criação de contas, autenticação, gerenciamento de clientes, depósitos, saques, transferências, investimentos, cálculo de imposto de renda e extrato bancário.

🚀 Versão Atual

v1.3.0

Esta versão introduz um módulo completo de investimentos, incluindo gerenciamento de carteira, resgate de investimentos, simulador de juros compostos e calculadora de imposto de renda, tornando a aplicação ainda mais próxima de um sistema bancário real.

🧠 Funcionalidades

* Criação de contas bancárias (Conta Corrente / Conta Poupança)
* Cadastro de clientes com validação de CPF
* Criação e autenticação segura de PIN
* Sistema de login da conta
* Exibição de clientes e contas cadastradas
* Geração automática do número da conta
* Prevenção de contas duplicadas para o mesmo cliente
* Depósitos
* Saques
* Transferências entre contas
* Extrato bancário
* Histórico de transações com data, valor e descrição
* Registro de transferências exibindo nome e número da conta do destinatário/remetente
* Controle de saldo
* Investimentos em renda fixa
* Gerenciamento da carteira de investimentos
* Resgate de investimentos
* Simulador de investimentos com juros compostos
* Calculadora de Imposto de Renda sobre investimentos

🏗️ Arquitetura

O projeto segue uma arquitetura modular baseada em serviços.

📁 Modelos

* Customer
* BankAccount
* Transaction
* Investment
* AccountType (enum)
* InvestmentType (enum)

⚙️ Serviços

* BankService → Controla o fluxo da aplicação e o menu principal
* CustomerService → Gerencia os clientes
* AccountService → Responsável pela criação e gerenciamento das contas
* AuthenticationService → Responsável pela autenticação e gerenciamento do PIN
* InvestmentService → Gerencia investimentos, carteira, resgates e simulações
* IncomeTaxService → Calcula o Imposto de Renda dos investimentos com base na tabela regressiva da renda fixa brasileira

🔄 Novidades da v1.3.0

* Adicionado módulo completo de investimentos
* Implementados investimentos em renda fixa
* Adicionada carteira de investimentos
* Implementado resgate de investimentos
* Adicionado simulador de investimentos com juros compostos
* Implementada calculadora de Imposto de Renda
* Melhorada a arquitetura do projeto com serviços dedicados aos investimentos
* Expansão das funcionalidades bancárias para gerenciamento de patrimônio

🗺️ Roadmap

✅ Concluído

* Cadastro de clientes
* Criação de contas bancárias
* Autenticação segura
* Conta Corrente e Conta Poupança
* Depósitos
* Saques
* Transferências
* Histórico de transações
* Extrato bancário
* Gerenciamento de investimentos
* Carteira de investimentos
* Resgate de investimentos
* Simulador de investimentos
* Calculadora de Imposto de Renda

🚧 Em Desenvolvimento

* Aprimoramento do histórico de transações
* Dashboard com resumo financeiro

📌 Planejado

* Persistência de dados (JSON)
* Integração com SQL Server
* Entity Framework Core
* Comparação de desempenho entre investimentos
* Aportes mensais em investimentos
* Metas financeiras
* Testes unitários
* Melhorias na interface do console
* API REST
* ASP.NET Core Web API
* Aplicativo mobile (Flutter)
* Deploy em nuvem

🛠️ Tecnologias

* C# (.NET 8)
* Console Application
* Programação Orientada a Objetos (POO)
* Arquitetura baseada em serviços

📌 Objetivo

Este projeto foi desenvolvido para fins de aprendizado e portfólio, com foco em:

* Princípios de Programação Orientada a Objetos (POO)
* Arquitetura limpa (Clean Architecture)
* Arquitetura baseada em serviços
* Princípios SOLID
* Simulação de regras de negócio de um sistema bancário real
* Desenvolvimento de sistemas financeiros

👨‍💻 Autor

Desenvolvido por Pedro Henrique dos Santos Dantas.
