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
    public class UserRepository : InterfaceRepository<User>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public UserRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<User>> GetAll()
        {
            return await _ShopDbContext.Users.ToListAsync();
        }


        public async Task<User?> GetItemById(int id)
        {
            var user = await _ShopDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            return user;

        }

        public async Task<User?> CreateItem(User user)
        {
            if (await _ShopDbContext.Users.AnyAsync(el => el.Email == user.Email))
            {
                return null;
            }

            await _ShopDbContext.Users.AddAsync(user);

            await _ShopDbContext.SaveChangesAsync();
            return user;
        }


        public async Task<User?> EditItem(int id, User user)
        {
            var oldUser = await _ShopDbContext.Users.FindAsync(id);
            if (oldUser == null)
            {
                return null;
            }
            if (await _ShopDbContext.Users.AnyAsync(el => el.Email == user.Email && el.UserId != id))
            {
                return null;
            }

            oldUser.Name = user.Name;
            oldUser.Surname = user.Surname;
            oldUser.Email = user.Email;
            oldUser.Password = user.Password;

            await _ShopDbContext.SaveChangesAsync();

            return user;

        }

        public async Task<User?> DeleteItem(int id)
        {
            var deletedUser = await _ShopDbContext.Users.FindAsync(id);
            if (deletedUser == null)
            {
                return null;
            }

            var ordersDel = await _ShopDbContext.Orders.Where(el => el.UserId == id).ToListAsync();

            foreach (var order in ordersDel)
            {
                var orderDetailsDel = await _ShopDbContext.OrderDetails.Where(el => el.OrderId == order.OrderId).ToListAsync();
                orderDetailsDel.ForEach(el => _ShopDbContext.OrderDetails.Remove(el));

                _ShopDbContext.Orders.Remove(order);
            }


            _ShopDbContext.Users.Remove(deletedUser);
            await _ShopDbContext.SaveChangesAsync();
            return deletedUser;

        }

    }
}
