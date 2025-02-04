﻿using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces.CategorySpecifics;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Repositories.Category
{
    public class CategoryRepository(AppDbContext context):ICategory
    {
        public async Task<IEnumerable<Product>>GetProductsByCategory(Guid categoryId)
        {
            var products = await context
                .Products
                .Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
            return products.Count > 0 ? products : [];                 
        }
    }
}
