using eCommerceApp.Application.Services.Implementations.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Implementations;
using eCommerceApp.Infrastructure.Middleware;
using eCommerceApp.Infrastructure.Repositories;
using eCommerceApp.Infrastructure.Repositories.Authentication;
using eCommerceApp.Infrastructure.Repositories.Cart;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace eCommerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration config)
        {
            string stripeSecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY")
                        ?? config["Stripe:SecretKey"]
                        ?? throw new InvalidOperationException("Stripe Secret Key is not configured");
           
            string connectionString = "Default";
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString(connectionString),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();
            })
            .UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            //services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            //services.AddScoped < IGeneric<Category>, GenericRepository<Category>>();
            services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;

            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
            });
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<ITokenManagement, TokenManagement>();
            services.AddScoped<IRoleManagement, RoleManagement>();
            services.AddScoped<IPaymentMethod, PaymentMethodRepository>();
            services.AddScoped<ICart, CartRepository>();
            services.AddScoped<IPaymentMethodsService, PaymentMethodService>();
            services.AddScoped<IPaymentService, StripePaymentService>();



            Stripe.StripeConfiguration.ApiKey = stripeSecretKey;
            return services;
        }
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}