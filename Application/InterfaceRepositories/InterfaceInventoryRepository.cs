using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface InterfaceInventoryRepository
    {
        Task<List<Inventory>> GetAll();

        Task<Inventory?> GetInventoryById(int id);

        Task<Inventory?> EditInventory(int id, Inventory inventory);

        Task<Inventory?> CreateInventory(Inventory inventory);

        Task<Inventory?> DeleteInventory(int id);
    }
}
