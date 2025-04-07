using BankAccountMicroservice.Models.Enums;
using System.Collections.Generic;

namespace BankAccountMicroservice.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public string Name { get; set; }
        public AccountStatus Status { get; set; }
        public decimal AvailableBalance { get; set; }

        public int AccountHolderId { get; set; }
        public AccountHolder AccountHolder { get; set; }

        public ICollection<Withdrawal> Withdrawals { get; set; } = new List<Withdrawal>();
        public ICollection<AccountAudit> AuditLogs { get; set; } = new List<AccountAudit>();
    }
}