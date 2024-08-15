using Application.Interfaces.IServices;
using Application.ViewModels.AccountDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Account
{
    [Authorize(Roles = "Admin")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountList()
        {
            var User = await _accountService.GetAccountAsync();
            return Ok(User);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var findAccountUser = await _accountService.GetAccountByIdAsync(id);
            return Ok(findAccountUser);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateAccountDTO createdAccountDTO)
        {
            var response = await _accountService.CreateAccountAsync(createdAccountDTO);
            return Created(nameof(CreateUser), response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccountDTO accountDTO)
        {
            var updatedUser = await _accountService.UpdateUserAsync(id, accountDTO);
            if (!updatedUser.Success)
            {
                return NotFound(updatedUser);
            }
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var deletedUser = await _accountService.DeleteUserAsync(id);
            if (!deletedUser.Success)
            {
                return NotFound(deletedUser);
            }

            return Ok(deletedUser);
        }

    }
}
