using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductCategoryService : InterfaceService<ProductCategory>
    {
        private readonly InterfaceRepository<ProductCategory> _ProductCategoryRepository;

        public ProductCategoryService(InterfaceRepository<ProductCategory> ProductCategoryRepository)
        {
            _ProductCategoryRepository = ProductCategoryRepository;
        }
        public async Task<List<ProductCategory>> GetAll()
        {
            return await _ProductCategoryRepository.GetAll();

        }

        public async Task<ProductCategory?> GetItemById(int id)
        {
            return await _ProductCategoryRepository.GetItemById(id);

        }

        public async Task<ProductCategory?> CreateItem(ProductCategory productCategory)
        {
            return await _ProductCategoryRepository.CreateItem(productCategory);

        }

        public async Task<ProductCategory?> EditItem(int id, ProductCategory productCategory)
        {
            return await _ProductCategoryRepository.EditItem(id, productCategory);

        }

        public async Task<ProductCategory?> DeleteItem(int id)
        {
            return await _ProductCategoryRepository.DeleteItem(id);

        }


    }
}
