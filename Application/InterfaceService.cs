using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Application
{
    public interface InterfaceService<T>
    {
        Task<List<T>> GetAll();

        Task<T?> GetItemById(int id);

        Task<T?> EditItem(int id, T item);

        Task<T?> CreateItem(T item);

        Task<T?> DeleteItem(int id);

    }
}
