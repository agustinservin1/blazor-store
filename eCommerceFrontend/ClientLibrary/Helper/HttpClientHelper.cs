using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helper
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

        public HttpClient GetPublicClientAsync()
        {
            return clientFactory.CreateClient(Constant.ApiClient.Name);
            
        }
    }
}
