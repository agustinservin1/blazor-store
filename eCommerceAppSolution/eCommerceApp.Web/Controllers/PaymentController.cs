using eCommerceApp.Application.Models.CartDto;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController (IPaymentMethodsService paymentMethods) : ControllerBase
    {
        [HttpGet("payment-methods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            var methods = await paymentMethods.GetPaymentMethods();
            return methods.Any() ? Ok(methods) : NotFound();
        }
    }
}
