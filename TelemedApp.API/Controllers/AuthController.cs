using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.Application.Requests.Auth;
using TelemedApp.Identity.Interfaces;

namespace TelemedApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IIdentityService identityService) : ControllerBase
    {
        private readonly IIdentityService _identityService = identityService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var token = await _identityService.RegisterAsync(request.Email, request.Password, request.FullName);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _identityService.LoginAsync(request.Email, request.Password);
            return Ok(new { token });
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var result = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);
            return Ok(new { token = result.Token, refreshToken = result.RefreshToken });
        }
    }
}