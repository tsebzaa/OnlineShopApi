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
    public class OrderRepository : InterfaceRepository<Order>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public OrderRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _ShopDbContext.Orders.ToListAsync();
        }


        public async Task<Order?> GetItemById(int id)
        {
            var order = await _ShopDbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }

            return order;

        }

        public async Task<Order?> CreateItem(Order order)
        {
            if(!await _ShopDbContext.Users.AnyAsync(el => el.UserId == order.UserId) || 
               !await _ShopDbContext.PaymentTypes.AnyAsync(el => el.PaymentId == order.PaymentId)) 
            {
                return null;
            }
            
            await _ShopDbContext.Orders.AddAsync(order);

            await _ShopDbContext.SaveChangesAsync();
            return order;
        }


        public async Task<Order?> EditItem(int id, Order order)
        {
            var oldOrder = await _ShopDbContext.Orders.FindAsync(id);
            if (oldOrder == null)
            {
                return null;
            }
            if (!await _ShopDbContext.Users.AnyAsync(el => el.UserId == order.UserId) ||
                !await _ShopDbContext.PaymentTypes.AnyAsync(el => el.PaymentId == order.PaymentId))
            {
                return null;
            }


            oldOrder.PaymentId = order.PaymentId;
            oldOrder.UserId = order.UserId;
            oldOrder.OrderDate = order.OrderDate;



            await _ShopDbContext.SaveChangesAsync();

            return order;

        }

        public async Task<Order?> DeleteItem(int id)
        {
            var deletedOrder = await _ShopDbContext.Orders.FindAsync(id);
            if (deletedOrder == null)
            {
                return null;
            }

            var orderDetailsDel = await _ShopDbContext.OrderDetails.Where(el => el.OrderId == id).ToListAsync();

            orderDetailsDel.ForEach(el => _ShopDbContext.OrderDetails.Remove(el));


            _ShopDbContext.Orders.Remove(deletedOrder);
            await _ShopDbContext.SaveChangesAsync();
            return deletedOrder;

        }

    }
}
