using BankAccountMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountMicroservice.Services.Interfaces
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccount>> GetAccountsByHolderAsync(int accountHolderId);
        Task<BankAccount> GetAccountByNumberAsync(string accountNumber);
        Task<WithdrawalResult> CreateWithdrawalAsync(string accountNumber, decimal amount);
    }

    public record WithdrawalResult(bool Success, string Message, Withdrawal Withdrawal = null);
}