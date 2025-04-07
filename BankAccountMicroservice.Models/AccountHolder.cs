using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountMicroservice.Models
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    }
}
