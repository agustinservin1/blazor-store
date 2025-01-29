using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.CategoryDto;
using eCommerceApp.Application.Models.ProductDTO;

namespace eCommerceApp.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategory createCategory);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(UpdateCategory updateCategory);
    }
}

