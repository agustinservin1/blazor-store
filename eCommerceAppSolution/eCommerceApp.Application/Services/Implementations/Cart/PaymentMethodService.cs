using AutoMapper;
using eCommerceApp.Application.Models.CartDto;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
 
namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper) : IPaymentMethodsService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
            var methods = await paymentMethod.GetPaymentsMethods();
            if (!methods.Any()) return [];
            return mapper.Map<IEnumerable<GetPaymentMethod>>(methods);   
        }
    }
}
