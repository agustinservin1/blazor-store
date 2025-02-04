using ClientLibrary.Models.Category;
using ClientLibrary.Models.Products;

namespace ClientLibrary.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategory createCategory);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(UpdateCategory updateCategory);
        Task<IEnumerable<GetProduct>> GetProductsByCategory(Guid categoryId);
    }
}
