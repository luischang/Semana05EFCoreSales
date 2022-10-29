using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Semana05EFCoreSales.DOMAIN.Core.DTOs;
using Semana05EFCoreSales.DOMAIN.Core.Interfaces;

namespace Semana05EFCoreSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _usersRepository.Login(email, password);
            if (user==null)
                return NotFound();
            var userDTO = _mapper.Map<UsersLoginDTO>(user);
            return Ok(userDTO);
        }
    }
}
