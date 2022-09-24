using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Semana05EFCoreSales.DOMAIN.Core.Entities;
using Semana05EFCoreSales.DOMAIN.Core.Interfaces;

namespace Semana05EFCoreSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpGet("GetByQueryParams")]
        public async Task<IActionResult> GetCustomerByQueryParamsId([FromQuery] int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            var result = await _customerRepository.Insert(customer);
            if (!result)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest("No concuerda la información del cliente");

            var result = await _customerRepository.Update(customer);
            if (!result)
                return BadRequest("Ocurrió un problema al actualizar el cliente");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerRepository.Delete(id);
            if (!result) 
                return BadRequest("Ocurrió un error al eliminar el cliente");

            return Ok(result);
        }

    }
}
