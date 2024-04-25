using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Application.InterfaceServices
{
    public interface InterfaceProductService
    {
        Task<List<Product>> GetAll();

        Task<Product?> GetProductById(int id);

        Task<Product?> EditProduct(int id, Product product);

        Task<Product?> CreateProduct(Product product);

        Task<Product?> DeleteProduct(int id);

    }
}
