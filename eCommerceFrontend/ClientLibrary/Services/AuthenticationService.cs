using ClientLibrary.Helper;
using ClientLibrary.Models;
using ClientLibrary.Models.Authentication;
namespace ClientLibrary.Services
{
    public class AuthenticationService(IApiCallHelper apiHelper, IHttpClientHelper httpClient) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.Register,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user,
            };
            var result = await apiHelper.ApiCallTypeCall<CreateUser>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }

        public async Task<LoginResponse> LoginUser(CreateUser user)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.Login,
                Type = Constant.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user,
            };
            var result = await apiHelper.ApiCallTypeCall<CreateUser>(apiCall);
            return result == null ? new LoginResponse(Message: apiHelper.ConnectionError().Message) :
                await apiHelper.GetServiceResponse<LoginResponse>(result);
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constant.Authentication.ReviveToken,
                Type = Constant.ApiCallType.Get,
                Client = client,
                Id = HttpUtility.UrlEncode(refreshToken),
                Model = null!
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result == null ? null! :
                await apiHelper.GetServiceResponse<LoginResponse>(result);
        }
    }
}
