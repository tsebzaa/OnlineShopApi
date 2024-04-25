using Application.InterfaceServices;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InterfaceInventoryService _service;

        public InventoryController(InterfaceInventoryService service)
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
            var Inventory = await _service.GetInventoryById(id);
            if (Inventory == null)
            {
                return BadRequest("Nie ma spisu o podanym id");
            }
            return Ok(Inventory);
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateInventory(Inventory inventory)
        {
            var createdInventory = await _service.CreateInventory(inventory);
            if (inventory == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdInventory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditInventory(int id, Inventory inventory)
        {
            var editedInventory = await _service.EditInventory(id, inventory);
            if (editedInventory == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedInventory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            var deletedInventory = await _service.DeleteInventory(id);
            if (deletedInventory == null)
            {
                return BadRequest("Nie ma spisu o podanym id");
            }
            return Ok(deletedInventory);
        }
    }
}
