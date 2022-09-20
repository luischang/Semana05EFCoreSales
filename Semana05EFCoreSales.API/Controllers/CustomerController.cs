using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
