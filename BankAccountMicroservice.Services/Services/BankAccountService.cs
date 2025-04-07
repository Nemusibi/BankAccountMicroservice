using BankAccountMicroservice.Data.Repositories;
using BankAccountMicroservice.Models;
using BankAccountMicroservice.Models.Enums;
using BankAccountMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankAccountMicroservice.Services.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BankAccountService> _logger;

        public BankAccountService(IUnitOfWork unitOfWork, ILogger<BankAccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<BankAccount>> GetAccountsByHolderAsync(int accountHolderId)
        {
            return await _unitOfWork.BankAccounts.Query()
                .Where(a => a.AccountHolderId == accountHolderId)
                .ToListAsync();
        }

        public async Task<BankAccount> GetAccountByNumberAsync(string accountNumber)
        {
            return await _unitOfWork.BankAccounts.Query()
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<WithdrawalResult> CreateWithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumberAsync(accountNumber);
            if (account == null)
                return new WithdrawalResult(false, "Account not found");

            // Validate withdrawal
            if (amount <= 0)
                return new WithdrawalResult(false, "Withdrawal amount must be greater than 0");

            if (amount > account.AvailableBalance)
                return new WithdrawalResult(false, "Insufficient funds");

            if (account.Status != AccountStatus.Active)
                return new WithdrawalResult(false, "Account is not active");

            if (account.Type == AccountType.FixedDeposit && amount != account.AvailableBalance)
                return new WithdrawalResult(false, "Only full withdrawals are allowed for Fixed Deposit accounts");

            // Process withdrawal
            account.AvailableBalance -= amount;
            var withdrawal = new Withdrawal
            {
                BankAccountId = account.Id,
                Amount = amount,
                TransactionDate = DateTime.UtcNow
            };

            // Log audit
            var audit = new AccountAudit
            {
                BankAccountId = account.Id,
                Action = "Withdrawal",
                Details = $"Withdrawal of {amount} processed",
                Timestamp = DateTime.UtcNow
            };

            try
            {
                await _unitOfWork.Withdrawals.AddAsync(withdrawal);
                await _unitOfWork.AccountAudits.AddAsync(audit);
                await _unitOfWork.BankAccounts.UpdateAsync(account);
                await _unitOfWork.CommitAsync();

                return new WithdrawalResult(true, "Withdrawal successful", withdrawal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing withdrawal");
                return new WithdrawalResult(false, "Error processing withdrawal");
            }
        }
    }
}