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
    public class OrderDetailRepository : InterfaceRepository<OrderDetail>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public OrderDetailRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<OrderDetail>> GetAll()
        {
            return await _ShopDbContext.OrderDetails.ToListAsync();
        }


        public async Task<OrderDetail?> GetItemById(int id)
        {
            var orderDetail = await _ShopDbContext.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return null;
            }

            return orderDetail;

        }

        public async Task<OrderDetail?> CreateItem(OrderDetail orderDetail)
        {
            if (!await _ShopDbContext.Orders.AnyAsync(el => el.OrderId == orderDetail.OrderId) ||
                !await _ShopDbContext.Products.AnyAsync(el => el.ProductId == orderDetail.ProductId))
            {
                return null;
            }
            if(orderDetail.Quantity < 0)
            {
                return null;
            }
            var product = await _ShopDbContext.Products.FindAsync(orderDetail.ProductId);
            var inventory = await _ShopDbContext.Inventories.FindAsync(product.InventoryId);  
            if(inventory.Amount - orderDetail.Quantity < 0) 
            {
                return null;
            }

            product.Inventory.Amount -= orderDetail.Quantity;

            await _ShopDbContext.OrderDetails.AddAsync(orderDetail);

            await _ShopDbContext.SaveChangesAsync();
            return orderDetail;
        }


        public async Task<OrderDetail?> EditItem(int id, OrderDetail orderDetail)
        {
            var oldOrderDetail = await _ShopDbContext.OrderDetails.FindAsync(id);
            if (oldOrderDetail == null)
            {
                return null;
            }
            if (!await _ShopDbContext.Orders.AnyAsync(el => el.OrderId == orderDetail.OrderId) ||
                !await _ShopDbContext.Products.AnyAsync(el => el.ProductId == orderDetail.ProductId))
            {
                return null;
            }
            if (orderDetail.Quantity < 0)
            {
                return null;
            }
            var product = await _ShopDbContext.Products.FindAsync(orderDetail.ProductId);
            var inventory = await _ShopDbContext.Inventories.FindAsync(product.InventoryId);
            if (inventory.Amount - orderDetail.Quantity < 0)
            {
                return null;
            }

            oldOrderDetail.ProductId = orderDetail.ProductId;
            oldOrderDetail.Quantity = orderDetail.Quantity;
            oldOrderDetail.OrderId = orderDetail.OrderId;


            await _ShopDbContext.SaveChangesAsync();

            return orderDetail;

        }

        public async Task<OrderDetail?> DeleteItem(int id)
        {
            var deletedOrderDetail = await _ShopDbContext.OrderDetails.FindAsync(id);
            if (deletedOrderDetail == null)
            {
                return null;
            }

            _ShopDbContext.OrderDetails.Remove(deletedOrderDetail);
            await _ShopDbContext.SaveChangesAsync();
            return deletedOrderDetail;

        }

    }
}
