using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.Models.CartDto
{
    public class Checkout
    {
        [Required]
        public required Guid PaymentMethodId { get; set; }
        [Required]
        public required IEnumerable<ProcessCart> Carts { get; set; }
    }
}
