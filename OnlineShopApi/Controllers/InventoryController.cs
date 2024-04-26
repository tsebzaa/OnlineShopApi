using Application;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InterfaceService<Inventory> _service;

        public InventoryController(InterfaceService<Inventory>  service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> Get()
        {
            return Ok(await _service.GetAll());

        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<Inventory>> GetInventoryById(int id)
        {
            var Inventory = await _service.GetItemById(id);
            if (Inventory == null)
            {
                return BadRequest("Nie ma spisu o podanym id");
            }
            return Ok(Inventory);
        }
        /*
        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateInventory(Inventory productCategory)
        {
            var createdInventory = await _service.CreateItem(productCategory);
            if (productCategory == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdInventory);
        }
        */
        [HttpPut("{id}")]
        public async Task<ActionResult> EditInventory(int id, Inventory productCategory)
        {
            var editedInventory = await _service.EditItem(id, productCategory);
            if (editedInventory == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedInventory);
        }
        /*
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            var deletedInventory = await _service.DeleteItem(id);
            if (deletedInventory == null)
            {
                return BadRequest("Nie ma spisu o podanym id");
            }
            return Ok(deletedInventory);
        }
        */
    }
}
