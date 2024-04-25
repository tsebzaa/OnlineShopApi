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
    public class InventoryRepository : InterfaceInventoryRepository
    {
  
        private readonly DevSafeRossContext _ShopDbContext;

        public InventoryRepository(DevSafeRossContext testDbContext)
        {
            _ShopDbContext = testDbContext;
        }

        public async Task<List<Inventory>> GetAll()
        {
            return  await _ShopDbContext.Inventories.ToListAsync();
        }


        public async Task<Inventory?> GetInventoryById(int id)
        {
            var Inventory =await  _ShopDbContext.Inventories.FindAsync(id);
            if(Inventory == null) 
            {
                return  null;
            }

            return  Inventory;

        }

        public async Task<Inventory?> CreateInventory(Inventory inventory)
        {
            await _ShopDbContext.AddAsync(inventory);
            await _ShopDbContext.SaveChangesAsync();
            return  inventory;
        }


        public async Task<Inventory?> EditInventory(int id,Inventory inventory) 
        {
            var oldInventory = await _ShopDbContext.Inventories.FindAsync(id);
            if(oldInventory == null) 
            {
                return null;
            }

            oldInventory.Amount = inventory.Amount;
            await _ShopDbContext.SaveChangesAsync();

            return inventory;

        }

        public async Task<Inventory?> DeleteInventory(int id)
        {
            var deletedInventory = await _ShopDbContext.Inventories.FindAsync(id);
            if(deletedInventory == null) 
            {
                return null; 
            }

            var productsDel = await _ShopDbContext.Products.Where(el => el.InventoryId == id).ToListAsync();
            productsDel.ForEach(el=> _ShopDbContext.Products.Remove(el));

            _ShopDbContext.Inventories.Remove(deletedInventory);
            await _ShopDbContext.SaveChangesAsync();
            return deletedInventory;

        }

    }
}
