using BankAccountMicroservice.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankAccountMicroservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountHoldersController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public AccountHoldersController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet("{accountHolderId}/accounts")]
        public async Task<ActionResult> GetAccounts(int accountHolderId)
        {
            var accounts = await _bankAccountService.GetAccountsByHolderAsync(accountHolderId);
            return Ok(accounts);
        }
    }
}