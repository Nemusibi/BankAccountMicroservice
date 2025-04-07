using BankAccountMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccountMicroservice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<AccountAudit> AccountAudits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.Withdrawals)
                .WithOne(w => w.BankAccount)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.AuditLogs)
                .WithOne(a => a.BankAccount)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data
            modelBuilder.Entity<AccountHolder>().HasData(SeedData.GetAccountHolders());
            modelBuilder.Entity<BankAccount>().HasData(SeedData.GetBankAccounts());
        }
    }
}