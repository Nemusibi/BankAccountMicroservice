using BankAccountMicroservice.Models;
using BankAccountMicroservice.Models.Enums;
using System.Collections.Generic;

namespace BankAccountMicroservice.Data
{
    public static class SeedData
    {
        public static List<AccountHolder> GetAccountHolders()
        {
            return new List<AccountHolder>
            {
                new AccountHolder
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1980, 1, 1),
                    IdNumber = "8001010000001",
                    ResidentialAddress = "123 Main St, Anytown",
                    MobileNumber = "0821234567",
                    Email = "john.doe@example.com"
                },
                new AccountHolder
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    IdNumber = "8505150000002",
                    ResidentialAddress = "456 Oak Ave, Somewhere",
                    MobileNumber = "0839876543",
                    Email = "jane.smith@example.com"
                }
            };
        }

        public static List<BankAccount> GetBankAccounts()
        {
            return new List<BankAccount>
            {
                new BankAccount
                {
                    Id = 1,
                    AccountNumber = "1000000001",
                    Type = AccountType.Cheque,
                    Name = "John's Cheque Account",
                    Status = AccountStatus.Active,
                    AvailableBalance = 15000.00m,
                    AccountHolderId = 1
                },
                new BankAccount
                {
                    Id = 2,
                    AccountNumber = "2000000001",
                    Type = AccountType.Savings,
                    Name = "Jane's Savings Account",
                    Status = AccountStatus.Active,
                    AvailableBalance = 25000.50m,
                    AccountHolderId = 2
                },
                new BankAccount
                {
                    Id = 3,
                    AccountNumber = "3000000001",
                    Type = AccountType.FixedDeposit,
                    Name = "John's Fixed Deposit",
                    Status = AccountStatus.Active,
                    AvailableBalance = 50000.00m,
                    AccountHolderId = 1
                }
            };
        }
    }
}