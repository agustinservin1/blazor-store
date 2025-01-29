using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProduct createProduct);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(UpdateProduct updateProduct);
    }
}

