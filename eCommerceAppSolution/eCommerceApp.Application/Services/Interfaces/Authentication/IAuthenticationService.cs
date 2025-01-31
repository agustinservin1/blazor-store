using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.IdentityDto;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser user);
        Task<LoginResponse> ReviveToken(string refreshToken);
    }
}
