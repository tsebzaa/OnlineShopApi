using Application;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly InterfaceTestService _service;

        public TestController(InterfaceTestService service)
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
            var Product = _service.CreateProduct(product);
            return Ok(Product);
        }

        [HttpPut]
        public ActionResult EditProduct(long id,Product product) 
        {
            var Eproduct = _service.EditProduct(product,id);
            return Ok(Eproduct);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var DeletedProduct = _service.DeleteProduct(id);
            return Ok(DeletedProduct);
        }

       
    }
}
