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
            string jwt = await tokenService.GetJwtTokenAsync(Constant.Cookie.Name);
            if (string.IsNullOrEmpty(jwt))
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claims = GetClaims(jwt);
            if (claims.Count == 0)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            return await Task.FromResult(new AuthenticationState(claimPrincipal));
        }
        public void NotifyAuthenticationState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        private static List<Claim> GetClaims(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims.ToList();



        }
    }
}
