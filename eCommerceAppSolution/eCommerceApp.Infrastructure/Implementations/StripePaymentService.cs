using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.CartDto;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Implementations
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> cartss)
        {
            try
            {

                var lineItems = new List<SessionLineItemOptions>();
                foreach (var item in cartProducts)
                {
                    var pQuantity = cartss.FirstOrDefault(_ => _.ProductId == item.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description,

                            },
                            UnitAmount = (long)(item.Price * 100),
                        },
                        Quantity = pQuantity!.Quantity,
                    });
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https:localhost:7267/payment-success",
                    CancelUrl = "https:localhost:7267/payment-cancel"
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServiceResponse(true, session.Url);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
