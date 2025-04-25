# 🏦 Bank Account Microservice

A scalable microservice for managing bank accounts, transactions (deposits/withdrawals), and balance operations. Built with .NET Core and Entity Framework.

## Features

- **Account Management**  
  ✅ Create/close accounts  
  ✅ Get account details  
  ✅ Check balances  

- **Transaction Processing**  
  💰 Deposit funds  
  💸 Withdraw funds (with overdraft protection)  
  🔄 Transfer between accounts  

- **Security**  
  🔒 Audit logging  
  🛑 Validation checks  

## Tech Stack

| Component       | Technology |
|-----------------|------------|
| Backend         | .NET 8     |
| Database        | SQL Server / PostgreSQL |
| ORM             | Entity Framework Core |
| Testing         | xUnit, Moq |
| API Documentation | Swagger |

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker (for containerized DB)

### Installation
```bash
git clone https://github.com/Nemusibi/BankAccountMicroservice.git
cd bank-account-microservice
dotnet restore
