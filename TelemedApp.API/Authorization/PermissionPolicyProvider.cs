using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace TelemedApp.API.Authorization
{
    public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallback = new(options);

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
            => _fallback.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
            => _fallback.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireClaim("permission", policyName)
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }
    }
}