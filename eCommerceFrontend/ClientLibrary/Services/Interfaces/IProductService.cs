using ClientLibrary.Models.Category;
using ClientLibrary.Models.Products;

namespace ClientLibrary.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProduct product);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(UpdateProduct product);
    }
}
