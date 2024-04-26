using Application;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly InterfaceService<ProductCategory> _service;

        public ProductCategoryController(InterfaceService<ProductCategory>  service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> Get()
        {
            return Ok(await _service.GetAll());

        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<ProductCategory>> GetProductCategoryById(int id)
        {
            var ProductCategory = await _service.GetItemById(id);
            if (ProductCategory == null)
            {
                return BadRequest("Nie ma kategorii o podanym id");
            }
            return Ok(ProductCategory);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> CreateProductCategory(ProductCategory productCategory)
        {
            var createdProductCategory = await _service.CreateItem(productCategory);
            if (productCategory == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdProductCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditProductCategory(int id, ProductCategory productCategory)
        {
            var editedProductCategory = await _service.EditItem(id, productCategory);
            if (editedProductCategory == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedProductCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductCategory(int id)
        {
            var deletedProductCategory = await _service.DeleteItem(id);
            if (deletedProductCategory == null)
            {
                return BadRequest("Nie ma kategorii o podanym id");
            }
            return Ok(deletedProductCategory);
        }
    }
}
