﻿using AutoMapper;
using eCommerceApp.Application.Models;
using eCommerceApp.Application.Models.ProductDTO;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations
{
    public class ProductService(IGeneric<Product> productInterface, IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var mappedData = mapper.Map<Product>(product);
            int result = await productInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product Added") :
                   new ServiceResponse(false, "Product failed to be Added");
        }
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var result = await productInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Product deleted") :
                new ServiceResponse(false, "Product failed to be deleted");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData = await productInterface.GetAllAsync();
            if (!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(rawData);

        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var rawData = await productInterface.GetByIdAsync(id);
            if (!rawData.Any()) return new GetProduct();
            return mapper.Map<GetProduct>(rawData);

        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var mappedData = mapper.Map<Product>(product);
            int result = await productInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product updated") :
                   new ServiceResponse(false, "Product failed to be updated");
        }
    }
}
