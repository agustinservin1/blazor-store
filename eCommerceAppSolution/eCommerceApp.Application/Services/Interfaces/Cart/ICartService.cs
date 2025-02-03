using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.CartDto;
using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> Checkout(Checkout checkout);
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
    }
}
