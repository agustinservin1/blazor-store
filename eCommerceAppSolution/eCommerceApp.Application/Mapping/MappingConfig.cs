using AutoMapper;
using eCommerceApp.Application.Models.CategoryDto;
using eCommerceApp.Application.Models.ProductDTO;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();
            
            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();

            
        }
    }
}
