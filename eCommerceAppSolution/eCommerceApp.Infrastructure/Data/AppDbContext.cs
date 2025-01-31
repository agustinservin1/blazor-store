using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Data
    {
        public class AppDbContext: IdentityDbContext<AppUser>
    {
            public AppDbContext(DbContextOptions options) : base(options)
            { 
           
            }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<RefreshToken> RefreshToken {  get; set; } 
        }
    }
