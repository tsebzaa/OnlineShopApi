using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InventoryService : InterfaceService<Inventory>
    {
        private readonly InterfaceRepository<Inventory> _InventoryRepository;

        public InventoryService(InterfaceRepository<Inventory> InventoryRepository)
        {
            _InventoryRepository = InventoryRepository;
        }
        public async Task<List<Inventory>> GetAll()
        {
            return await _InventoryRepository.GetAll();

        }

        public async Task<Inventory?> GetItemById(int id)
        {
            return await _InventoryRepository.GetItemById(id);

        }

        public async Task<Inventory?> CreateItem(Inventory inventory)
        {
            return await _InventoryRepository.CreateItem(inventory);

        }

        public async Task<Inventory?> EditItem(int id, Inventory inventory)
        {
            return await _InventoryRepository.EditItem(id, inventory);

        }

        public async Task<Inventory?> DeleteItem(int id)
        {
            return await _InventoryRepository.DeleteItem(id);

        }


    }
}
