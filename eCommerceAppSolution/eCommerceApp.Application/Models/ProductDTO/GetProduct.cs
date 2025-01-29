namespace eCommerceApp.Application.Models.ProductDTO
{
    public class GetProduct : ProductBase
    {
        public Guid Id { get; set; }
        public GetCategory Category { get; set; } = new();
    }
   
}
