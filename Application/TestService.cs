using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class TestService : InterfaceTestService
    {
        private readonly InterfaceTestRepository _testRepository;

        public TestService(InterfaceTestRepository testRepository)
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

        public Product EditProduct(Product product, long id) 
        {
            _testRepository.EditProduct(product, id);
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = _testRepository.DeleteProduct(id);
            return product;
        }


    }
}
