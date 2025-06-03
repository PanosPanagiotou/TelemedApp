using Microsoft.AspNetCore.Mvc;
using TelemedApp.Identity.Services;
using System.Threading.Tasks;

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
    }

    public record RegisterRequest(string Email, string Password, string FullName);
    public record LoginRequest(string Email, string Password);
}
