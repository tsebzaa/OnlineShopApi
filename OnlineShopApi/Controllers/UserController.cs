using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application;


namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InterfaceService<User> _service;

        public UserController(InterfaceService<User> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>>  Get()
        {
            return Ok(await _service.GetAll());
       
        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user =await _service.GetItemById(id);
            if (user == null)
            {
                return BadRequest("Nie ma użytkownika o podanym id");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user) 
        {
            var createdUser = await _service.CreateItem(user);
            if (user == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditUser(int id,User user)
        { 
            var editedUser =await _service.EditItem(id,user);
            if (editedUser == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deletedUser =await _service.DeleteItem(id);
            if (deletedUser == null)
            {
                return BadRequest("Nie ma użytkownika o podanym id");
            }
            return Ok(deletedUser);
        }

       
    }
}
