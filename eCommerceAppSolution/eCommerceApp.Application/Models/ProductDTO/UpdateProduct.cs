using eCommerceApp.Application.Models.CategoryDto;
using System.IO.Pipes;

namespace eCommerceApp.Application.Models.ProductDTO
{
    public class UpdateProduct : CategoryBase
    {
        public Guid Id { get; set; }
    }
}