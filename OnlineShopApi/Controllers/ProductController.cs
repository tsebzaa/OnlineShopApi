using Application;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public ActionResult<List<Product>>  Get()
        {
            var products = _service.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}/GetById")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _service.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> PostTest(Product product) 
        {
            var createdProduct = _service.CreateProduct(product);
            return Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public ActionResult EditProduct(int id,Product product)
        { 

            var editedProduct = _service.EditProduct(id,product);
            return Ok(editedProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var deletedProduct = _service.DeleteProduct(id);
            return Ok(deletedProduct);
        }

       
    }
}
