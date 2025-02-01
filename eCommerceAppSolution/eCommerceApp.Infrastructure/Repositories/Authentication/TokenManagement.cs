using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement
    {
        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            string token = Convert.ToBase64String(randomBytes);
            return WebUtility.UrlEncode(token);
        }
        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null)
            {
                return jwtToken.Claims.ToList();
            }
            return [];
        }
        public async Task<string> GetUserIdByRefreshToken(string refreshToken)
        {
            string refreshTokenHash = HashRefreshToken(refreshToken);
            var tokenRecord = await context.RefreshToken.FirstOrDefaultAsync(rf => rf.RefreshTokenHash == refreshTokenHash);
            return tokenRecord?.UserId ?? string.Empty; 
        }
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            return await HandleRefreshToken(userId, refreshToken);
        }

        private async Task<int> HandleRefreshToken(string userId, string token)
        {
            var executionStrategy = context.Database.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                var newToken = new RefreshToken
                {
                    UserId = userId,
                    RefreshTokenHash = HashRefreshToken(token),
                };

                context.RefreshToken.Add(newToken);
                await context.SaveChangesAsync(); 

                await DeleteOldRefreshTokens(userId, newToken.Id);
            });

            return 1; 
        }
        public async Task<int> UpdateRefreshToken(string userId, string newRefreshToken)
        {
            return await HandleRefreshToken(userId, newRefreshToken);
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            string refreshTokenHash = HashRefreshToken(refreshToken);
            var tokenRecord = await context.RefreshToken.FirstOrDefaultAsync(
                rt => rt.RefreshTokenHash == HashRefreshToken(refreshToken));

            if (tokenRecord == null || IsTokenExpired(tokenRecord))
            {
                return false;
            }
            return true;
        }
        private static bool IsTokenExpired(RefreshToken token)
        {
            return token.ExpiresAt < DateTime.UtcNow;
        }
        private static string HashRefreshToken(string refreshToken)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            return Convert.ToBase64String(hashedBytes);
        }
        private async Task DeleteOldRefreshTokens(string userId, Guid currentTokenId)
        {
            var tokens = await context.RefreshToken
                .Where(rt => rt.UserId == userId && rt.Id != currentTokenId) 
                .OrderByDescending(rt => rt.CreatedAt) 
                .Skip(4) 
                .ToListAsync();

            if (tokens.Any())
            {
                context.RefreshToken.RemoveRange(tokens);
                await context.SaveChangesAsync();
            }
        }


    }
}
