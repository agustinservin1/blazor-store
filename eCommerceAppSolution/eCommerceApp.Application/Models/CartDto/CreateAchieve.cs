using System.ComponentModel.DataAnnotations;
namespace eCommerceApp.Application.Models.CartDto
{
    public class CreateAchieve
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Guid UserId { get; set; }

    }
}
