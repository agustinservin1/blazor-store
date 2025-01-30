using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.ProductDTO;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct?> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProduct createProduct);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(UpdateProduct updateProduct);
    }
}

