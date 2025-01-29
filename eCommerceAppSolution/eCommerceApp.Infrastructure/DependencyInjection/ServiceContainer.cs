﻿using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "Default";
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString(connectionString),
            sqlOptions =>
            {
                //Ensure this is the correct assembly
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();//Enable automatic retries for transient failures
            }),
            ServiceLifetime.Scoped);

            //services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            //services.AddScoped < IGeneric<Category>, GenericRepository<Category>>();
            services.AddScoped(typeof(IGeneric<>));

            return services;

        }
    }
}