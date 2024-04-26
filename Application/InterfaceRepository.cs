using Domain.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface InterfaceRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T?> GetItemById(int id);

        Task<T?> EditItem(int id, T item);

        Task<T?> CreateItem(T item);

        Task<T?> DeleteItem(int id);

    }
}
