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
    public class InventoryRepository : InterfaceRepository<Inventory>
    {

        private readonly DevSafeRossContext _ShopDbContext;

        public InventoryRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<Inventory>> GetAll()
        {
            return await _ShopDbContext.Inventories.ToListAsync();
        }


        public async Task<Inventory?> GetItemById(int id)
        {
            var Inventory = await _ShopDbContext.Inventories.FindAsync(id);
            if (Inventory == null)
            {
                return null;
            }

            return Inventory;

        }

        public async Task<Inventory?> CreateItem(Inventory inventory)
        {
            await _ShopDbContext.AddAsync(inventory);
            await _ShopDbContext.SaveChangesAsync();
            return inventory;
        }


        public async Task<Inventory?> EditItem(int id, Inventory inventory)
        {
            var oldInventory = await _ShopDbContext.Inventories.FindAsync(id);
            if (oldInventory == null)
            {
                return null;
            }
            if(inventory.Amount < 0)
            {
                return null;
            }
            oldInventory.Amount = inventory.Amount;

            await _ShopDbContext.SaveChangesAsync();

            return inventory;

        }

        public async Task<Inventory?> DeleteItem(int id)
        {
            var deletedInventory = await _ShopDbContext.Inventories.FindAsync(id);
            if (deletedInventory == null)
            {
                return null;
            }

            var productsDel = await _ShopDbContext.Products.Where(el => el.InventoryId == id).ToListAsync();
            productsDel.ForEach(el => _ShopDbContext.Products.Remove(el));

            _ShopDbContext.Inventories.Remove(deletedInventory);
            await _ShopDbContext.SaveChangesAsync();
            return deletedInventory;

        }

    }
}
