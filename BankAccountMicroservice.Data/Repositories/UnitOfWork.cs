using BankAccountMicroservice.Models;

namespace BankAccountMicroservice.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        private IRepository<AccountHolder> _accountHolders;
        public IRepository<AccountHolder> AccountHolders =>
            _accountHolders ??= new Repository<AccountHolder>(_context);

        private IRepository<BankAccount> _bankAccounts;
        public IRepository<BankAccount> BankAccounts =>
            _bankAccounts ??= new Repository<BankAccount>(_context);

        private IRepository<Withdrawal> _withdrawals;
        public IRepository<Withdrawal> Withdrawals =>
            _withdrawals ??= new Repository<Withdrawal>(_context);

        private IRepository<AccountAudit> _accountAudits;
        public IRepository<AccountAudit> AccountAudits =>
            _accountAudits ??= new Repository<AccountAudit>(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}