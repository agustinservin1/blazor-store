using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.Models.CategoryDto
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }

    }
}