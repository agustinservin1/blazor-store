using eCommerceApp.Application.Models.CategoryDto;

namespace eCommerceApp.Application.Models.ProductDTO
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}