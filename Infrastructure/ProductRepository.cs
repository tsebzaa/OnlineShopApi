using Application;
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

namespace Infrastructure
{
    public class ProductRepository : InterfaceProductRepository
    {
  
        private readonly DevSafeRossContext _ShopDbContext;

        public ProductRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public List<Product> GetAll()
        {
            return _ShopDbContext.Products.ToList();
        }


        public Product GetProductById(int id)
        {
            var product = _ShopDbContext.Products.Find(id);
            if(product == null) 
            {
                return null;
            }

            return product;

        }

        public Product CreateProduct(Product product)
        {
            product.OrderDetails = new List<OrderDetail> { new OrderDetail() {} };
            _ShopDbContext.Products.Add(product);
            _ShopDbContext.SaveChanges();
            return product;
        }


        public Product EditProduct(int id,Product product) 
        {
            var oldProduct = _ShopDbContext.Products.Find(id);
            if(oldProduct == null) 
            {
                return null;
            }
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            oldProduct.InventoryId = product.InventoryId;
            oldProduct.ProductCategoryId = product.ProductCategoryId;

            _ShopDbContext.SaveChanges();

            return product;

        }

        public Product DeleteProduct(int id)
        {
            var deletedProduct = _ShopDbContext.Products.Find(id);
            if(deletedProduct == null) 
            {
                return null; 
            }

            var ordersDel = _ShopDbContext.OrderDetails.Where(el => el.ProductId == id).ToList();
            ordersDel.ForEach(el=> _ShopDbContext.OrderDetails.Remove(el));

            _ShopDbContext.Products.Remove(deletedProduct);
            _ShopDbContext.SaveChanges();
            return deletedProduct;

        }

    }
}
