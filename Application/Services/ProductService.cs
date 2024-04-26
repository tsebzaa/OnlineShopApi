using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : InterfaceService<Product>
    {
        private readonly InterfaceRepository<Product> _productRepository;

        public ProductService(InterfaceRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();

        }

        public async Task<Product?> GetItemById(int id)
        {
            return await _productRepository.GetItemById(id);

        }

        public async Task<Product?> CreateItem(Product product)
        {
            return await _productRepository.CreateItem(product);

        }

        public async Task<Product?> EditItem(int id, Product product)
        {
            return await _productRepository.EditItem(id, product);

        }

        public async Task<Product?> DeleteItem(int id)
        {
            return await _productRepository.DeleteItem(id);

        }


    }
}
