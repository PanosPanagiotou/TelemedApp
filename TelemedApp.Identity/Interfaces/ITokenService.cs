using TelemedApp.Identity.Models;

namespace TelemedApp.Identity.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResult> GenerateJwtToken(ApplicationUser user);
        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken);
    }
}