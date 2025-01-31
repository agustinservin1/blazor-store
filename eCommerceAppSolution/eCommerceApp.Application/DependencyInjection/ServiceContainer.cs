﻿using eCommerceApp.Application.Mapping;
using eCommerceApp.Application.Services.Implementations;
using eCommerceApp.Application.Services.Implementations.Authentication;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Application.Validations;
using eCommerceApp.Application.Validations.Authentication;
using eCommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddAplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;

        }
    }
}
