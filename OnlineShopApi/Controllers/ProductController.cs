using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.InterfaceServices;


namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InterfaceProductService _service;

        public ProductController(InterfaceProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>>  Get()
        {
            return Ok(await _service.GetAll());
       
        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product =await _service.GetProductById(id);
            if (product == null)
            {
                return BadRequest("Nie ma produktu o podanym id");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product) 
        {
            var createdProduct = await _service.CreateProduct(product);
            if (product == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditProduct(int id,Product product)
        { 
            var editedProduct =await _service.EditProduct(id,product);
            if (editedProduct == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deletedProduct =await _service.DeleteProduct(id);
            if (deletedProduct == null)
            {
                return BadRequest("Nie ma produktu o podanym id");
            }
            return Ok(deletedProduct);
        }

       
    }
}
