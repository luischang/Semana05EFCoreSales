using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Semana05EFCoreSales.DOMAIN.Core.DTOs;
using Semana05EFCoreSales.DOMAIN.Core.Entities;
using Semana05EFCoreSales.DOMAIN.Core.Interfaces;

namespace Semana05EFCoreSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetCustomers();
            ////Convert customer to customerDTO
            //var customerList = new List<CustomerDTO>();
            //foreach (var item in customers)
            //{
            //    customerList.Add(new CustomerDTO
            //    {
            //        Id = item.Id,
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        City = item.City,
            //        Country = item.Country,
            //        Phone = item.Phone
            //    });
            //}
            var customerList = _mapper.Map<List<CustomerDTO>>(customers);
            return Ok(customerList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }

        [HttpGet("GetByQueryParams")]
        public async Task<IActionResult> GetCustomerByQueryParamsId([FromQuery] int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
                return NotFound();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);

            var result = await _customerRepository.Insert(customer);
            if (!result)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDTO customerDTO)
        {
            if (id != customerDTO.Id)
                return BadRequest("No concuerda la información del cliente");

            var customer = _mapper.Map<Customer>(customerDTO);

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

        [HttpGet("GetCustomerOrdersByCustomerId/{id}")]
        public async Task<IActionResult> GetCustomerOrdersByCustomerId(int id)
        {
            var customer = await _customerRepository.GetCustomersWithOrders(id);
            if (customer == null)
                return NotFound();

            var customerDTO = _mapper.Map<CustomerOrderDTO>(customer);
            return Ok(customerDTO);
        }

    }
}
