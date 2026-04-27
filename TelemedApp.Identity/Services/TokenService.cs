using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TelemedApp.Identity.Authorization;
using TelemedApp.Identity.Data;
using TelemedApp.Identity.Interfaces;
using TelemedApp.Identity.Models;

namespace TelemedApp.Identity.Services
{
    public class TokenService(
        IConfiguration config,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IdentityDbContext db) : ITokenService
    {
        private readonly IConfiguration _config = config;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly IdentityDbContext _db = db;

        public async Task<TokenResult> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var accessTokenMinutes = jwtSettings.GetValue<int>("AccessTokenMinutes", 30);
            var refreshTokenDays = jwtSettings.GetValue<int>("RefreshTokenDays", 7);

            // Load roles for the user
            var roles = await _userManager.GetRolesAsync(user);

            // Base claims
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new("fullName", user.FullName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Role claims
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // Permission claims from role claims
            var permissionValues = new HashSet<string>();

            foreach (var role in roles)
            {
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity == null)
                    continue;

                var roleClaims = await _roleManager.GetClaimsAsync(roleEntity);

                foreach (var c in roleClaims.Where(c => c.Type == "permission"))
                {
                    // Avoid duplicate permission claims
                    if (permissionValues.Add(c.Value))
                        claims.Add(new Claim("permission", c.Value));
                }
            }

            // Feature flags (role-based + user-specific)
            var featureFlags = new HashSet<string>();

            // Add role-based feature flags
            foreach (var role in roles)
            {
                if (RoleFeatureFlags.Map.TryGetValue(role, out var roleFeatures))
                    featureFlags.UnionWith(roleFeatures); // Merge without duplicates
            }

            // Add user-specific feature flags (optional)
            featureFlags.UnionWith(user.UserFeatureFlags);

            // Emit feature claims
            foreach (var f in featureFlags)
                claims.Add(new Claim("feature", f));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenMinutes),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Create refresh token
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenDays)
            };

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();

            return new TokenResult
            {
                Success = true,
                Token = jwt,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<TokenResult> RefreshTokenAsync(string token, string refreshToken)
        {
            // Find stored refresh token
            var stored = await _db.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (stored == null || !stored.IsActive)
            {
                return new TokenResult
                {
                    Success = false,
                    Errors = ["Invalid or expired refresh token"]
                };
            }

            var user = await _userManager.FindByIdAsync(stored.UserId);
            if (user == null)
            {
                stored.RevokedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                return new TokenResult
                {
                    Success = false,
                    Errors = ["User not found"]
                };
            }

            // Invalidate if password changed after this refresh token was issued
            if (user.PasswordLastChangedAt > stored.CreatedAt)
            {
                stored.RevokedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                return new TokenResult
                {
                    Success = false,
                    Errors = ["Password changed. Please login again."]
                };
            }

            // Revoke old refresh token
            stored.RevokedAt = DateTime.UtcNow;

            // Issue new tokens (access + refresh)
            var newTokens = await GenerateJwtToken(user);

            await _db.SaveChangesAsync();

            return newTokens;
        }
    }
}