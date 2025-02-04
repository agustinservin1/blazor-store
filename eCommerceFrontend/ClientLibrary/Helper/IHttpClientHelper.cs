namespace ClientLibrary.Helper
{
    public interface IHttpClientHelper 
    {
        Task<HttpClient> GetPrivateClientAsync();
        HttpClient GetPublicClient();

    }
}
