using ClientLibrary.Models.Products;
namespace ClientLibrary.Models.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}
