using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.Models.ProductDTO
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; } = new();
    }
   
}
