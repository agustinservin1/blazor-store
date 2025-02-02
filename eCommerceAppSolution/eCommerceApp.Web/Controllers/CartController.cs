using eCommerceApp.Application.Models.CartDto;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Checkout(Checkout checkout)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await cartService.Checkout(checkout); 
            return result.Succes?Ok(result):BadRequest(result);
        }
        [HttpPost("save-checkout")]
        public async Task<IActionResult> SaveCheckout (IEnumerable<CreateAchieve> achieves)
        {
            var result = await cartService.SaveCheckoutHistory(achieves);
            return result.Succes ? Ok(result) : BadRequest(result);
        }

    }
}
