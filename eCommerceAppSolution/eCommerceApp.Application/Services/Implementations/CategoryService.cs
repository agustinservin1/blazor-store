using AutoMapper;
using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.CategoryDto;
using eCommerceApp.Application.Models.ProductDTO;
using eCommerceApp.Application.Services;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper) : ICategoryService{
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await categoryInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category Added") :
                    new ServiceResponse(false, "Category failed to be Added");
        }
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var result = await categoryInterface.DeleteAsync(id);
                return result > 0 ? new ServiceResponse(true, "Product deleted") :
                    new ServiceResponse(false, "Product failed to be deleted");
        }
        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await categoryInterface.GetAllAsync();
            if (!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetCategory>>(rawData);

        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await categoryInterface.GetByIdAsync(id);
            if (!rawData.Any()) return new GetCategory();
            return mapper.Map<GetCategory>(rawData); 
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await categoryInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category Updated") :
                    new ServiceResponse(false, "Category failed to be Updated");
        }
    }
    }
}
