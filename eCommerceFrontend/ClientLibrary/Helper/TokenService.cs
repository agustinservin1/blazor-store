using NetcodeHub.Packages.WebAssembly.Storage.Cookie;
using System.ComponentModel.Design;
namespace ClientLibrary.Helper
{
    public class TokenService(IBrowserCookieStorageService cookieService) : ITokenService
    {
        public async Task<string> GetRefreshTokenAsync(string key)
        {
            return await GetTokenAsync(key,1);
        }

        public async Task<string> GetJwtTokenAsync(string key)
        {
            return await GetTokenAsync(key, 0);
        }

        private async Task<string> GetTokenAsync(string key, int position)
        {
            try
            {
                string token = await cookieService.GetAsync(key);
                return token != null ? token.Split("--")[position] : null!; 
            } catch
            {
                return null!;
            }
        }

        public string FormToken(string jwt, string refresh)
        {
            return $"{jwt} -- {refresh}";
        }
        public async Task RemoveCookie(string key)
        {
            await cookieService.RemoveAsync(key);
        }
        public async Task SetCookie(string key, string value, int days, string path)
        {
            await cookieService.SetAsync(key, value, days, path);
        }
    }
}
