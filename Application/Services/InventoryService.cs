using Application.InterfaceRepositories;
using Application.InterfaceServices;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InventoryService : InterfaceInventoryService
    {
        private readonly InterfaceInventoryRepository _InventoryRepository;

        public InventoryService(InterfaceInventoryRepository InventoryRepository)
        {
            _InventoryRepository = InventoryRepository;
        }
        public async Task<List<Inventory>> GetAll()
        {
            return await _InventoryRepository.GetAll();

        }

        public async Task<Inventory?> GetInventoryById(int id)
        {
            return await _InventoryRepository.GetInventoryById(id);

        }

        public async Task<Inventory?> CreateInventory(Inventory inventory)
        {
            return await _InventoryRepository.CreateInventory(inventory);

        }

        public async Task<Inventory?> EditInventory(int id, Inventory inventory)
        {
            return await _InventoryRepository.EditInventory(id, inventory);

        }

        public async Task<Inventory?> DeleteInventory(int id)
        {
            return await _InventoryRepository.DeleteInventory(id);

        }


    }
}
