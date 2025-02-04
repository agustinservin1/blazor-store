using ClientLibrary.Models.Category;
using System.ComponentModel.DataAnnotations;
namespace ClientLibrary.Models.Products
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsNew => DateTime.Now <= CreatedDate.AddDays(7);    

    }
}
