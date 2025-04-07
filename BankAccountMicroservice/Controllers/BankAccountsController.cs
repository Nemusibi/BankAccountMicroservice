using BankAccountMicroservice.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankAccountMicroservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet("{accountNumber}")]
        public async Task<ActionResult> GetAccount(string accountNumber)
        {
            var account = await _bankAccountService.GetAccountByNumberAsync(accountNumber);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost("{accountNumber}/withdrawals")]
        public async Task<ActionResult> CreateWithdrawal(string accountNumber, [FromBody] decimal amount)
        {
            var result = await _bankAccountService.CreateWithdrawalAsync(accountNumber, amount);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}