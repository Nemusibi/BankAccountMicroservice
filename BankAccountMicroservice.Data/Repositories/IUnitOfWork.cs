using BankAccountMicroservice.Models;
using System.Threading.Tasks;

namespace BankAccountMicroservice.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<AccountHolder> AccountHolders { get; }
        IRepository<BankAccount> BankAccounts { get; }
        IRepository<Withdrawal> Withdrawals { get; }
        IRepository<AccountAudit> AccountAudits { get; }
        Task<int> CommitAsync();
    }
}