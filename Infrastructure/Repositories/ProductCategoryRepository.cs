using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Timers;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Application;

namespace Infrastructure.Repositories
{
    public class ProductCategoryRepository : InterfaceRepository<ProductCategory>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public ProductCategoryRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<ProductCategory>> GetAll()
        {
            return await _ShopDbContext.ProductCategories.ToListAsync();
        }


        public async Task<ProductCategory?> GetItemById(int id)
        {
            var ProductCategory = await _ShopDbContext.ProductCategories.FindAsync(id);
            if (ProductCategory == null)
            {
                return null;
            }

            return ProductCategory;

        }

        public async Task<ProductCategory?> CreateItem(ProductCategory productCategory)
        {
            if (await _ShopDbContext.ProductCategories.AnyAsync(el => el.Name == productCategory.Name))
            {
                return null;
            }


            await _ShopDbContext.AddAsync(productCategory);
            await _ShopDbContext.SaveChangesAsync();
            return productCategory;
        }


        public async Task<ProductCategory?> EditItem(int id, ProductCategory productCategory)
        {
            var oldProductCategory = await _ShopDbContext.ProductCategories.FindAsync(id);
            if (oldProductCategory == null)
            {
                return null;
            }
            if (await _ShopDbContext.ProductCategories.AnyAsync(el => el.Name == productCategory.Name))
            {
                return null;
            }

            oldProductCategory.Name = productCategory.Name;
            await _ShopDbContext.SaveChangesAsync();

            return productCategory;

        }

        public async Task<ProductCategory?> DeleteItem(int id)
        {
            var deletedProductCategory = await _ShopDbContext.ProductCategories.FindAsync(id);
            if (deletedProductCategory == null)
            {
                return null;
            }

            var productsDel = await _ShopDbContext.Products.Where(el => el.ProductCategoryId == id).ToListAsync();
            productsDel.ForEach(el => _ShopDbContext.Products.Remove(el));

            _ShopDbContext.ProductCategories.Remove(deletedProductCategory);
            await _ShopDbContext.SaveChangesAsync();
            return deletedProductCategory;

        }

    }
}
