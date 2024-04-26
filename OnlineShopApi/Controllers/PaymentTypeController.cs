using Application;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly InterfaceService<PaymentType> _service;

        public PaymentTypeController(InterfaceService<PaymentType>  service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentType>>> Get()
        {
            return Ok(await _service.GetAll());

        }

        [HttpGet("{id}/GetById")]
        public async Task<ActionResult<PaymentType>> GetPaymentTypeById(int id)
        {
            var PaymentType = await _service.GetItemById(id);
            if (PaymentType == null)
            {
                return BadRequest("Nie ma płatności o podanym id");
            }
            return Ok(PaymentType);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentType>> CreatePaymentType(PaymentType paymentType)
        {
            var createdPaymentType = await _service.CreateItem(paymentType);
            if (paymentType == null)
            {
                return BadRequest("Złe dane");
            }

            return Ok(createdPaymentType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPaymentType(int id, PaymentType paymentType)
        {
            var editedPaymentType = await _service.EditItem(id, paymentType);
            if (editedPaymentType == null)
            {
                return BadRequest("Złe dane");
            }
            return Ok(editedPaymentType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentType(int id)
        {
            var deletedPaymentType = await _service.DeleteItem(id);
            if (deletedPaymentType == null)
            {
                return BadRequest("Nie ma płatności o podanym id");
            }
            return Ok(deletedPaymentType);
        }
    }
}
