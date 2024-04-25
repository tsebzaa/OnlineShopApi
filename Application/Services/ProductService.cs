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
    public class ProductService : InterfaceProductService
    {
        private readonly InterfaceProductRepository _productRepository;

        public ProductService(InterfaceProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();

        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);

        }

        public async Task<Product?> CreateProduct(Product product)
        {
            return await _productRepository.CreateProduct(product);

        }

        public async Task<Product?> EditProduct(int id, Product product)
        {
            return await _productRepository.EditProduct(id, product);

        }

        public async Task<Product?> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);

        }


    }
}
