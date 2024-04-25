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
using Application.InterfaceRepositories;

namespace Infrastructure
{
    public class ProductRepository : InterfaceProductRepository
    {
  
        private readonly DevSafeRossContext _ShopDbContext;

        public ProductRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            return  await _ShopDbContext.Products.ToListAsync();
        }


        public async Task<Product?> GetProductById(int id)
        {
            var product =await  _ShopDbContext.Products.FindAsync(id);
            if(product == null) 
            {
                return  null;
            }

            return  product;

        }

        public async Task<Product?> CreateProduct(Product product)
        {
            if(await _ShopDbContext.Products.AnyAsync(el => el.Name == product.Name))
            {
                return null;
            }
            if(!await _ShopDbContext.Inventories.AnyAsync(el => el.InventoryId == product.InventoryId) ||
               !await _ShopDbContext.ProductCategories.AnyAsync(el => el.ProductCategoryId == product.ProductCategoryId) )
            {
                return null;
            }
            product.OrderDetails = new List<OrderDetail> { new OrderDetail() {} };
            await _ShopDbContext.Products.AddAsync(product);
            await _ShopDbContext.SaveChangesAsync();

            var ordersDel = await _ShopDbContext.OrderDetails.Where(el => el.ProductId == product.ProductId).ToListAsync();
            ordersDel.ForEach(el => _ShopDbContext.OrderDetails.Remove(el));
            await _ShopDbContext.SaveChangesAsync();
            return  product;
        }


        public async Task<Product?> EditProduct(int id,Product product) 
        {
            var oldProduct = await _ShopDbContext.Products.FindAsync(id);
            if(oldProduct == null) 
            {
                return null;
            }
            if (!await _ShopDbContext.Inventories.AnyAsync(el => el.InventoryId == product.InventoryId) ||
                !await _ShopDbContext.ProductCategories.AnyAsync(el => el.ProductCategoryId == product.ProductCategoryId))
            {
                return null;
            }

            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            oldProduct.InventoryId = product.InventoryId;
            oldProduct.ProductCategoryId = product.ProductCategoryId;

            await _ShopDbContext.SaveChangesAsync();

            return product;

        }

        public async Task<Product?> DeleteProduct(int id)
        {
            var deletedProduct = await _ShopDbContext.Products.FindAsync(id);
            if(deletedProduct == null) 
            {
                return null; 
            }

            var ordersDel = await _ShopDbContext.OrderDetails.Where(el => el.ProductId == id).ToListAsync();
            ordersDel.ForEach(el=> _ShopDbContext.OrderDetails.Remove(el));

            _ShopDbContext.Products.Remove(deletedProduct);
            await _ShopDbContext.SaveChangesAsync();
            return deletedProduct;

        }

    }
}
