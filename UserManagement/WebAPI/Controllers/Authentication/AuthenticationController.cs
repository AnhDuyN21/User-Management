using Application.Interfaces.IServices;
using Application.ViewModels.AccountDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Authentication
{

    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync( RegisterDTO registerObject)
        {
            var result = await _authenticationService.RegisterAsync(registerObject);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDTO loginObject)
        {
            var result = await _authenticationService.LoginAsync(loginObject);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(
                    new
                    {
                        success = result.Success,
                        message = result.Message,
                        token = result.Data
                    }
                );
            }
        }
    }
}
