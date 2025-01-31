using System.Security.Claims;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface ITokenManagement
    {
        Task<int> AddRefreshToken(string userId, string refreshToken);
        string GenerateToken(List<Claim> claims);
        string GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<string>GetUserIdByRefreshToken(string refreshToken);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<int> UpdateRefreshToken(string userId, string refreshToken);
    }
}
