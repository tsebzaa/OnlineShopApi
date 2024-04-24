using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ProductService : InterfaceProductService
    {
        private readonly InterfaceProductRepository _testRepository;

        public ProductService(InterfaceProductRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public List<Product> GetAll()
        {
            var products = _testRepository.GetAll();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _testRepository.GetProductById(id);
            return product;
        }


        public Product CreateProduct(Product product)
        {
            var createdProduct =  _testRepository.CreateProduct(product);
            return createdProduct;
        }

        public Product EditProduct(int id, Product product) 
        {
            _testRepository.EditProduct( id,product);
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = _testRepository.DeleteProduct(id);
            return product;
        }


    }
}
