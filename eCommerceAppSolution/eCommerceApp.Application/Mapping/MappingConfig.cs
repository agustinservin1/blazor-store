using AutoMapper;
using eCommerceApp.Application.Models.CartDto;
using eCommerceApp.Application.Models.CategoryDto;
using eCommerceApp.Application.Models.IdentityDto;
using eCommerceApp.Application.Models.ProductDTO;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;

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


            CreateMap<UpdateProduct, Product>();
            CreateMap<UpdateCategory, Category>();

            CreateMap<CreateUser, AppUser>();
            CreateMap<AppUser, CreateUser>();

            CreateMap<LoginUser, AppUser>();
            CreateMap<AppUser, LoginUser>();

            CreateMap<PaymentMethod, GetPaymentMethod>();

            CreateMap<CreateAchieve, Achieve>();
            CreateMap<Achieve, CreateAchieve>();



        }
    }
}
