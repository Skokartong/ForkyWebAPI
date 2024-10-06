using ForkyWebAPI.Models;
using ForkyWebAPI.Models.AccountDTOs;
using ForkyWebAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register([FromBody] NewAccountDTO accountDTO)
        {
            try
            {
                await _accountService.AddAccountAsync(accountDTO);
                return Ok("Account created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/deleteaccount/{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            await _accountService.DeleteAccountAsync(accountId);
            return NoContent();
        }

        [HttpPut]
        [Route("/updateaccount/{accountId}")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] UpdateAccountDTO accountDTO)
        {
            try
            {
                await _accountService.UpdateAccountAsync(accountId, accountDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/viewaccounts")]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet]
        [Route("/getaccount/{accountId}")]
        public async Task<ActionResult<Account?>> GetAccountById(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found");
            }
            return Ok(account);
        }
    }
}
