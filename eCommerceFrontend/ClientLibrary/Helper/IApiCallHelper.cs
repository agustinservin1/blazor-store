namespace ClientLibrary.Helper
{
    public interface IApiCallHelper
    {
        Task<HttpResponseMessage> ApiCallTypeCall<TModel>(ApiCall apiCall);
        Task<TResponse> GetServiceResponse<TResponse>(HttpResponseMessage message);
        ServiceResponse ConnectionError();

    }
}
