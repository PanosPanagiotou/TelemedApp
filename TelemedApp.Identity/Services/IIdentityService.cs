namespace TelemedApp.Identity.Services
{
    public interface IIdentityService
    {
        Task<string> RegisterAsync(string email, string password, string fullName);
        Task<string> LoginAsync(string email, string password);
    }
}
