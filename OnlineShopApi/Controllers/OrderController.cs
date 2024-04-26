using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application;


namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly InterfaceService<Order> _service;

        public OrderController(InterfaceService<Order> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>>  Get()
        {
            return Ok(await _service.GetAll());
       
        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order =await _service.GetItemById(id);
            if (order == null)
            {
                return BadRequest("Nie ma zamówienia o podanym id");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order) 
        {
            var createdOrder = await _service.CreateItem(order);
            if (order == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditOrder(int id,Order order)
        { 
            var editedOrder =await _service.EditItem(id,order);
            if (editedOrder == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var deletedOrder =await _service.DeleteItem(id);
            if (deletedOrder == null)
            {
                return BadRequest("Nie ma zamówienia o podanym id");
            }
            return Ok(deletedOrder);
        }

       
    }
}
