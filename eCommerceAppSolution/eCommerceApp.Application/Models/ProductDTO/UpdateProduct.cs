using eCommerceApp.Application.Models.CategoryDto;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace eCommerceApp.Application.Models.ProductDTO
{
    public class UpdateProduct : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}