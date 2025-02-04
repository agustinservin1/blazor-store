using ClientLibrary.Helper.Interfaces;
using System.Net.Http.Headers;

namespace ClientLibrary.Helper.Implementations
{
    public class HttpClientHelper(IHttpClientFactory clientFactory, ITokenService tokenService) : IHttpClientHelper
    {
        public async Task<HttpClient> GetPrivateClientAsync()
        {
            var client = clientFactory.CreateClient(Constant.ApiClient.Name);
            string token = await tokenService.GetJwtTokenAsync(Constant.Cookie.Name);
            if (string.IsNullOrEmpty(token)) return client;
            var newClient = new HttpClient();
            newClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(Constant.Authentication.Type);
            return newClient;
        }

        public HttpClient GetPublicClient()
        {
            return clientFactory.CreateClient(Constant.ApiClient.Name);

        }
    }
}
