namespace BankAccountMicroservice.Models
{
    public class AccountAudit
    {
        public int Id { get; set; }
        public string Action { get; set; } // "Create", "Update", "Delete", "Withdrawal"
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}