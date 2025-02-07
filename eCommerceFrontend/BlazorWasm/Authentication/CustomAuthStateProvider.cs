using ClientLibrary.Helper;
using ClientLibrary.Helper.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorWasm.Authentication
{
    public class CustomAuthStateProvider(ITokenService tokenService) : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                 string jwt = await tokenService.GetJwtTokenAsync(Constant.Cookie.Name);
                if (string.IsNullOrEmpty(jwt))
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claims = GetClaims(jwt);
                if (!claims.Any())
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public void NotifyAuthenticationState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        private static List<Claim> GetClaims(string jwt)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                
                Console.WriteLine($"JWT: {jwt}");
                var tokenParts = jwt.Split("--");
                var tokenOnly = tokenParts[0].Trim();

                var token = handler.ReadJwtToken(tokenOnly);
                var claims = token.Claims.ToList();

                foreach (var claim in claims)
                {
                    Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                }

                return claims;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Claim>();
            }
        }
    }
}
