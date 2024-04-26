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
    public class PaymentTypeRepository : InterfaceRepository<PaymentType>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public PaymentTypeRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<PaymentType>> GetAll()
        {
            return await _ShopDbContext.PaymentTypes.ToListAsync();
        }


        public async Task<PaymentType?> GetItemById(int id)
        {
            var PaymentType = await _ShopDbContext.PaymentTypes.FindAsync(id);
            if (PaymentType == null)
            {
                return null;
            }

            return PaymentType;

        }

        public async Task<PaymentType?> CreateItem(PaymentType paymentType)
        {
            if (await _ShopDbContext.PaymentTypes.AnyAsync(el => el.Name == paymentType.Name))
            {
                return null;
            }


            await _ShopDbContext.AddAsync(paymentType);
            await _ShopDbContext.SaveChangesAsync();
            return paymentType;
        }


        public async Task<PaymentType?> EditItem(int id, PaymentType paymentType)
        {
            var oldPaymentType = await _ShopDbContext.PaymentTypes.FindAsync(id);
            if (oldPaymentType == null)
            {
                return null;
            }
            if (await _ShopDbContext.PaymentTypes.AnyAsync(el => el.Name == paymentType.Name))
            {
                return null;
            }

            oldPaymentType.Name = paymentType.Name;
            await _ShopDbContext.SaveChangesAsync();

            return paymentType;

        }

        public async Task<PaymentType?> DeleteItem(int id)
        {
            var deletedPaymentType = await _ShopDbContext.PaymentTypes.FindAsync(id);
            if (deletedPaymentType == null)
            {
                return null;
            }

            var ordersDel = await _ShopDbContext.Orders.Where(el => el.PaymentId == id).ToListAsync();
            foreach(var order in ordersDel) 
            {
                var orderDetailsDel = await _ShopDbContext.OrderDetails.Where(el=> el.OrderId == order.OrderId).ToListAsync();
                orderDetailsDel.ForEach(el => _ShopDbContext.OrderDetails.Remove(el));

                _ShopDbContext.Orders.Remove(order);
            
            }

            _ShopDbContext.PaymentTypes.Remove(deletedPaymentType);
            await _ShopDbContext.SaveChangesAsync();
            return deletedPaymentType;

        }

    }
}
