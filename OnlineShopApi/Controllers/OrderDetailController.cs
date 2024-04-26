using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application;


namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly InterfaceService<OrderDetail> _service;

        public OrderDetailController(InterfaceService<OrderDetail> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDetail>>>  Get()
        {
            return Ok(await _service.GetAll());
       
        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetailById(int id)
        {
            var orderDetail =await _service.GetItemById(id);
            if (orderDetail == null)
            {
                return BadRequest("Nie ma szczegółu zamówienia o podanym id");
            }
            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail) 
        {
            var createdOrderDetail = await _service.CreateItem(orderDetail);
            if (orderDetail == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdOrderDetail);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditOrderDetail(int id,OrderDetail orderDetail)
        { 
            var editedOrderDetail =await _service.EditItem(id,orderDetail);
            if (editedOrderDetail == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedOrderDetail);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetail(int id)
        {
            var deletedOrderDetail =await _service.DeleteItem(id);
            if (deletedOrderDetail == null)
            {
                return BadRequest("Nie ma szczegółu zamówienia o podanym id");
            }
            return Ok(deletedOrderDetail);
        }

       
    }
}
