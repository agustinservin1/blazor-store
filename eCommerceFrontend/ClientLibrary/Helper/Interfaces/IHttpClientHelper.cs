namespace ClientLibrary.Helper.Interfaces
{
    public interface IHttpClientHelper
    {
        Task<HttpClient> GetPrivateClientAsync();
        HttpClient GetPublicClient();

    }
}
