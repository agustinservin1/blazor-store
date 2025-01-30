﻿using AutoMapper;
using eCommerceApp.Application.Models.CategoryDto;
using eCommerceApp.Application.Models.ProductDTO;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateProduct, Product>();
            CreateMap<Product, GetProduct>();
            CreateMap<UpdateProduct, Product>();


            CreateMap<CreateCategory, Category>();
            CreateMap<Category, GetCategory>();
            CreateMap<UpdateCategory, Category>();



        }
    }
}
