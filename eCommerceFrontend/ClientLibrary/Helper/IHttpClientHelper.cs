namespace ClientLibrary.Helper
{
    public interface IHttpClientHelper 
    {
        HttpClient GetHttpClient();
        Task<HttpClient> GetPrivateClientAsync();

    }
}
