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
    public class ProductRepository : InterfaceRepository<Product>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public ProductRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _ShopDbContext.Products.ToListAsync();
        }


        public async Task<Product?> GetItemById(int id)
        {
            var product = await _ShopDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            return product;

        }

        public async Task<Product?> CreateItem(Product product)
        {
            if (await _ShopDbContext.Products.AnyAsync(el => el.Name == product.Name))
            {
                return null;
            }
            
            if (!await _ShopDbContext.ProductCategories.AnyAsync(el => el.ProductCategoryId == product.ProductCategoryId))
            {
                return null;
            }

            var inventory = new Inventory();
            inventory.Amount = product.Inventory.Amount;


            await _ShopDbContext.Products.AddAsync(product);
            await _ShopDbContext.SaveChangesAsync();
   
            return product;
        }


        public async Task<Product?> EditItem(int id, Product product)
        {
            var oldProduct = await _ShopDbContext.Products.FindAsync(id);
            if (oldProduct == null)
            {
                return null;
            }
            if (!await _ShopDbContext.ProductCategories.AnyAsync(el => el.ProductCategoryId == product.ProductCategoryId))
            {
                return null;
            }

            if (await _ShopDbContext.Products.AnyAsync(el => el.Name == product.Name && el.ProductId != id))
            {
                return null;
            }

            var oldInventory = await _ShopDbContext.Inventories.FindAsync(oldProduct.InventoryId);

            
           
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            oldProduct.ProductCategoryId = product.ProductCategoryId;
            oldProduct.Inventory = oldInventory;
            oldProduct.Inventory.Amount = product.Inventory.Amount;
            
            await _ShopDbContext.SaveChangesAsync();

            return product;

        }

        public async Task<Product?> DeleteItem(int id)
        {
            var deletedProduct = await _ShopDbContext.Products.FindAsync(id);
            if (deletedProduct == null)
            {
                return null;
            }

            var ordersDel = await _ShopDbContext.OrderDetails.Where(el => el.ProductId == id).ToListAsync();
            ordersDel.ForEach(el => _ShopDbContext.OrderDetails.Remove(el));


            _ShopDbContext.Inventories.Remove(await _ShopDbContext.Inventories.FindAsync(deletedProduct.InventoryId));
            _ShopDbContext.Products.Remove(deletedProduct);
            await _ShopDbContext.SaveChangesAsync();
            return deletedProduct;

        }

    }
}
