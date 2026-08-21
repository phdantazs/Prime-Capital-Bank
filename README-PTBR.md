🏦 Prime Capital Bank

Um sistema bancário desenvolvido em C# .NET Console, simulando operações bancárias e financeiras, como criação de contas, autenticação, gerenciamento de clientes, depósitos, saques, transferências, investimentos, cálculo de Imposto de Renda, extrato bancário e operações com Bitcoin.

O projeto tem como objetivo simular, de forma prática, regras de negócio e fluxos encontrados em sistemas financeiros reais, com foco em organização de código, validações, tratamento de casos de borda e evolução incremental da aplicação.

🚀 Versão Atual

v1.4.0

Esta versão amplia o sistema com a introdução do módulo de Bitcoin, mantendo os módulos bancário e de investimentos e expandindo o projeto para diferentes tipos de operações financeiras.

Também foram realizados aprimoramentos nas regras de negócio, validações, autenticação e organização dos serviços.

🧠 Funcionalidades

🏦 Operações Bancárias

* Criação de contas bancárias (Conta Corrente / Conta Poupança)
* Cadastro de clientes com validação de CPF
* Geração automática do número da conta
* Prevenção de contas duplicadas para o mesmo cliente
* Limitação de contas por cliente
* Autenticação por PIN
* Login da conta
* Alteração de PIN
* Bloqueio temporário após tentativas inválidas de autenticação
* Depósitos
* Saques
* Transferências entre contas
* Controle de saldo
* Extrato bancário
* Histórico de transações
* Registro de data, valor e descrição das transações
* Identificação do destinatário/remetente em transferências

📈 Investimentos

* Investimentos em renda fixa
* Tesouro Selic
* CDB
* LCI
* LCA
* Fundo de Renda Fixa
* Gerenciamento da carteira de investimentos
* Visualização dos investimentos
* Resgate de investimentos
* Cálculo do valor atualizado dos investimentos
* Cálculo de rendimento
* Simulador de investimentos
* Juros compostos
* Aportes periódicos na simulação
* Comparação entre investimentos
* Perfil recomendado para cada investimento
* Informações sobre risco, liquidez, tributação e proteção do FGC
* Cálculo de Imposto de Renda
* Aplicação da tabela regressiva de IR para renda fixa
* Isenção de IR para LCI e LCA
* Desconto do imposto no resgate

₿ Bitcoin

* Abertura de conta Bitcoin
* Carteira Bitcoin
* Compra de Bitcoin
* Venda de Bitcoin
* Consulta de saldo em Bitcoin
* Consulta do preço do Bitcoin
* Histórico de transações Bitcoin
* Fechamento da conta Bitcoin

O módulo de Bitcoin encontra-se em evolução e algumas funcionalidades ainda estão sendo aprimoradas.

🏗️ Arquitetura

O projeto segue uma arquitetura modular baseada em serviços, buscando separar responsabilidades e manter as regras de negócio organizadas.

📁 Modelos

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

⚙️ Serviços

* BankService → Controla o fluxo da aplicação e os menus principais
* CustomerService → Gerencia clientes
* AccountService → Responsável pela criação e gerenciamento das contas
* AuthenticationService → Responsável pela autenticação, validação e gerenciamento do PIN
* InvestmentService → Gerencia investimentos, carteira, resgates e simulações
* TaxService → Calcula o Imposto de Renda dos investimentos
* BitcoinService → Gerencia carteira, compras, vendas e transações de Bitcoin
* InputService → Centraliza entradas e validações de dados do usuário

🔄 Novidades da v1.4.0

* Adicionado módulo de Bitcoin
* Implementada abertura de conta Bitcoin
* Implementada carteira Bitcoin
* Implementadas operações de compra e venda de Bitcoin
* Adicionado histórico de transações Bitcoin
* Adicionada consulta de saldo em Bitcoin
* Integrado o módulo Bitcoin ao fluxo principal do banco
* Aprimoradas validações de entrada
* Evolução das regras de negócio e tratamento de casos de borda
* Expansão do sistema para operações bancárias, investimentos e criptoativos

🗺️ Roadmap

✅ Concluído

* Cadastro de clientes
* Criação de contas bancárias
* Autenticação por PIN
* Bloqueio temporário após tentativas inválidas
* Conta Corrente e Conta Poupança
* Depósitos
* Saques
* Transferências
* Histórico de transações
* Extrato bancário
* Gerenciamento de investimentos
* Carteira de investimentos
* Investimentos em renda fixa
* Resgate de investimentos
* Simulador de investimentos
* Juros compostos
* Calculadora de Imposto de Renda
* Módulo Bitcoin
* Carteira Bitcoin
* Compra e venda de Bitcoin
* Histórico de transações Bitcoin

🚧 Em Desenvolvimento

* Aprimoramento do módulo Bitcoin
* Melhorias e padronização do InputService
* Tratamento de erros e robustez da aplicação
* Refatoração e revisão geral do sistema
* Melhorias na interface do console

📌 Próximos Passos

* Persistência de dados
* Integração com SQL Server
* Entity Framework Core
* Testes unitários
* Mensageria e notificações
* APIs
* ASP.NET Core Web API
* Integração com serviços externos
* Armazenamento em nuvem
* Deploy em nuvem
* Aplicativo mobile com Flutter

🛠️ Tecnologias

* C#
* .NET 8
* Console Application
* Programação Orientada a Objetos (POO)
* Arquitetura baseada em serviços
* Git
* GitHub

Futuras tecnologias

* SQL Server
* Entity Framework Core
* ASP.NET Core Web API
* Serviços de mensageria
* Cloud
* Flutter

📌 Objetivo

Este projeto foi desenvolvido para aprendizado e portfólio, com foco na construção progressiva de um sistema financeiro e na aplicação prática de conceitos de desenvolvimento de software.

Principais objetivos:

* Praticar Programação Orientada a Objetos
* Aplicar princípios SOLID
* Desenvolver e organizar regras de negócio
* Trabalhar com separação de responsabilidades
* Simular operações financeiras reais
* Trabalhar com validações e casos de borda
* Desenvolver sistemas financeiros em C#
* Evoluir a arquitetura de uma aplicação de forma incremental
* Utilizar Git e GitHub durante o desenvolvimento

👨‍💻 Autor

Desenvolvido por Pedro Henrique dos Santos Dantas.
