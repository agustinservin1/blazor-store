using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.Models.CategoryDto
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}